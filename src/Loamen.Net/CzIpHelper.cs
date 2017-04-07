using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Loamen.Net
{
    /// <summary>
    /// 纯真IP帮助类
    /// </summary>
    public class CzIpHelper
    {
        protected FileStream FileStrm;
        protected UInt32 IndexCount;
        protected UInt32 IndexEnd;
        protected UInt32 IndexSet;
        private CzIndexInfo _searchEnd;
        protected UInt32 SearchIndexEnd;
        protected UInt32 SearchIndexSet;
        private CzIndexInfo _searchMid;
        private CzIndexInfo _searchSet;
        private bool _bFilePathInitialized;

        public CzIpHelper(string dbFilePath)
        {
            _bFilePathInitialized = false;
            SetDbFilePath(dbFilePath);
        }


        //使用二分法查找索引区，初始化查找区间
        public void Initialize()
        {
            SearchIndexSet = 0;
            SearchIndexEnd = IndexCount - 1;
        }

        //关闭文件
        public void Dispose()
        {
            if (_bFilePathInitialized)
            {
                _bFilePathInitialized = false;
                FileStrm.Close();
                //FileStrm.Dispose();
            }
        }


        public bool SetDbFilePath(string dbFilePath)
        {
            if (dbFilePath == "")
            {
                return false;
            }

            try
            {
                FileStrm = new FileStream(dbFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch
            {
                return false;
            }
            //检查文件长度
            if (FileStrm.Length < 8)
            {
                FileStrm.Close();
                //FileStrm.Dispose();
                return false;
            }
            //得到第一条索引的绝对偏移和最后一条索引的绝对偏移
            FileStrm.Seek(0, SeekOrigin.Begin);
            IndexSet = GetUInt32();
            IndexEnd = GetUInt32();

            //得到总索引条数
            IndexCount = (IndexEnd - IndexSet)/7 + 1;
            _bFilePathInitialized = true;

            return true;
        }

        public string GetAddressWithIp(string ipValue)
        {
            if (!_bFilePathInitialized)
            {
                return "";
            }

            Initialize();

            var ip = IptoUInt32(ipValue);

            while (true)
            {
                //首先初始化本轮查找的区间

                //区间头
                _searchSet = IndexInfoAtPos(SearchIndexSet);
                //区间尾
                _searchEnd = IndexInfoAtPos(SearchIndexEnd);

                //判断IP是否在区间头内
                if (ip >= _searchSet.IpSet && ip <= _searchSet.IpEnd)
                    return ReadAddressInfoAtOffset(_searchSet.Offset);


                //判断IP是否在区间尾内
                if (ip >= _searchEnd.IpSet && ip <= _searchEnd.IpEnd)
                    return ReadAddressInfoAtOffset(_searchEnd.Offset);

                //计算出区间中点
                _searchMid = IndexInfoAtPos((SearchIndexEnd + SearchIndexSet)/2);

                //判断IP是否在中点
                if (ip >= _searchMid.IpSet && ip <= _searchMid.IpEnd)
                    return ReadAddressInfoAtOffset(_searchMid.Offset);

                //本轮没有找到，准备下一轮
                if (ip < _searchMid.IpSet)
                    //IP比区间中点要小，将区间尾设为现在的中点，将区间缩小1倍。
                    SearchIndexEnd = (SearchIndexEnd + SearchIndexSet)/2;
                else
                    //IP比区间中点要大，将区间头设为现在的中点，将区间缩小1倍。
                    SearchIndexSet = (SearchIndexEnd + SearchIndexSet)/2;
            }

            //return "";
        }

        public bool IpAddressCheck(string addressString)
        {
            try
            {
                var r = new Regex(
                    @"(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])");
                string webServerAddress = addressString;
                webServerAddress = webServerAddress.Trim();
                var m = r.Match(webServerAddress);
                if (m.Success) //进一步判断IP地址的合法性
                {
                    var charArray = new[] {'.'};
                    int j = 0;
                    var stringArray = webServerAddress.Split(charArray);
                    foreach (string str in stringArray)
                    {
                        if (int.Parse(str) <= 255)
                            j++;
                        else
                            break;
                    }
                    return j == 4;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private string ReadAddressInfoAtOffset(UInt32 offset)
        {
            string country;
            string area;
            UInt32 countryOffset;
            //跳过4字节，因这4个字节是该索引的IP区间上限。
            FileStrm.Seek(offset + 4, SeekOrigin.Begin);

            //读取一个字节，得到描述国家信息的“寻址方式”
            byte tag = GetTag();

            switch (tag)
            {
                case 0x01:
                    FileStrm.Seek(GetOffset(), SeekOrigin.Begin);
                    tag = GetTag();
                    if (tag == 0x02)
                    {
                        //模式0x02，表示接下来的3个字节代表国家信息的偏移位置
                        //先将这个偏移位置保存起来，因为我们要读取它后面的地区信息。
                        countryOffset = GetOffset();
                        //读取地区信息（注：按照Luma的说明，好像没有这么多种可能性，但在测试过程中好像有些情况没有考虑到，
                        //所以写了个ReadArea()来读取。
                        area = ReadArea();
                        //读取国家信息
                        FileStrm.Seek(countryOffset, SeekOrigin.Begin);
                        country = ReadString();
                    }
                    else
                    {
                        //这种模式说明接下来就是保存的国家和地区信息了，以'\0'代表结束。
                        FileStrm.Seek(-1, SeekOrigin.Current);
                        country = ReadString();
                        area = ReadArea();
                    }
                    break;
                case 0x02:
                    countryOffset = GetOffset();
                    area = ReadArea();
                    FileStrm.Seek(countryOffset, SeekOrigin.Begin);
                    country = ReadString();
                    break;
                default:
                    FileStrm.Seek(-1, SeekOrigin.Current);
                    country = ReadString();
                    area = ReadArea();
                    break;
            }
            var address = country + " " + area;
            return address;
        }

        private UInt32 GetOffset()
        {
            var tempByte4 = new byte[4];
            tempByte4[0] = (byte) FileStrm.ReadByte();
            tempByte4[1] = (byte) FileStrm.ReadByte();
            tempByte4[2] = (byte) FileStrm.ReadByte();
            tempByte4[3] = 0;
            return BitConverter.ToUInt32(tempByte4, 0);
        }

        protected string ReadArea()
        {
            var tag = GetTag();

            if (tag == 0x01 || tag == 0x02)
            {
                FileStrm.Seek(GetOffset(), SeekOrigin.Begin);
                return ReadString();
            }
            FileStrm.Seek(-1, SeekOrigin.Current);
            return ReadString();
        }

        protected string ReadString()
        {
            UInt32 offset = 0;
            var tempByteArray = new byte[256];
            tempByteArray[offset] = (byte) FileStrm.ReadByte();
            while (tempByteArray[offset] != 0x00)
            {
                offset += 1;
                tempByteArray[offset] = (byte) FileStrm.ReadByte();
            }
            return Encoding.Default.GetString(tempByteArray).TrimEnd('\0');
        }

        protected byte GetTag()
        {
            return (byte) FileStrm.ReadByte();
        }

        private CzIndexInfo IndexInfoAtPos(UInt32 indexPos)
        {
            var indexInfo = new CzIndexInfo();
            //根据索引编号计算出在文件中在偏移位置

            FileStrm.Seek(IndexSet + 7*indexPos, SeekOrigin.Begin);
            indexInfo.IpSet = GetUInt32();
            indexInfo.Offset = GetOffset();
            FileStrm.Seek(indexInfo.Offset, SeekOrigin.Begin);
            indexInfo.IpEnd = GetUInt32();

            return indexInfo;
        }

        public UInt32 IptoUInt32(string ipValue)
        {
            string[] ipByte = ipValue.Split('.');
            var nUpperBound = ipByte.GetUpperBound(0);
            if (nUpperBound != 3)
            {
                ipByte = new string[4];
                for (var i = 1; i <= 3 - nUpperBound; i++)
                    ipByte[nUpperBound + i] = "0";
            }

            var tempByte4 = new byte[4];
            for (var i = 0; i <= 3; i++)
            {
                //'如果是.Net 2.0可以支持TryParse。
                //'If Not (Byte.TryParse(IpByte(i), TempByte4(3 - i))) Then
                //'    TempByte4(3 - i) = &H0
                //'End If
                if (IsNumeric(ipByte[i]))
                {
                    tempByte4[3 - i] = (byte) (Convert.ToInt32(ipByte[i]) & 0xff);
                }
            }

            return BitConverter.ToUInt32(tempByte4, 0);
        }

        protected bool IsNumeric(string str)
        {
            if (str != null && Regex.IsMatch(str, @"^-?\d+$"))
            {
                return true;
            }
            return false;
        }

        protected UInt32 GetUInt32()
        {
            var tempByte4 = new byte[4];
            FileStrm.Read(tempByte4, 0, 4);
            return BitConverter.ToUInt32(tempByte4, 0);
        }
    }

    internal class CzIndexInfo
    {
        public UInt32 IpEnd;
        public UInt32 IpSet;
        public UInt32 Offset;

        public CzIndexInfo()
        {
            IpSet = 0;
            IpEnd = 0;
            Offset = 0;
        }
    }
}