using cn.bmob.io;
using Loamen.PluginFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyHero.Entity
{
    public class ProxyServers : BmobTable
    {
        //对应要操作的数据表
        public const String TABLE_NAME = "ProxyServers";

        private BmobInt _status = -1;
        private string _testDate = DateTime.UtcNow.ToString();
        private string __type = "HTTP";
        private String fTable;

        public ProxyServers():this(TABLE_NAME)
        {
        }

        public ProxyServers(String tableName)
        {
            this.fTable = tableName;
        }

        public override string table
        {
            get
            {
                if (fTable != null)
                {
                    return fTable;
                }
                return base.table;
            }
        }

        /// <summary>
        ///     代理IP
        /// </summary>
        public string proxy { get; set; }

        /// <summary>
        ///     代理端口
        /// </summary>
        public BmobInt port { get; set; }

        /// <summary>
        ///     代理类型
        /// </summary>
        public string type
        {
            get { return __type; }
            set { __type = value; }
        }

        /// <summary>
        ///     响应速度
        /// </summary>
        public BmobInt response { get; set; }

        /// <summary>
        ///     代理账户
        /// </summary>
        public string proxyusername { get; set; }

        /// <summary>
        ///     代理密码
        /// </summary>
        public string proxypassword { get; set; }

        /// <summary>
        ///     匿名度
        /// </summary>
        public string anonymity { get; set; }

        /// <summary>
        ///     国家
        /// </summary>
        public string country { get; set; }

        /// <summary>
        ///     匿名度
        /// </summary>
        public string anonymityen { get; set; }

        /// <summary>
        ///     国家
        /// </summary>
        public string countryen { get; set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string description { get; set; }

        /// <summary>
        ///     测试时间
        /// </summary>
        public string testdate
        {
            get { return _testDate; }
            set { _testDate = value; }
        }

        /// <summary>
        ///     //状态0:dead,1:alive,2:not test
        /// </summary>
        public BmobInt status
        {
            get { return _status; }
            set { _status = value; }
        }

        /// <summary>
        ///     提供用户
        /// </summary>
        public string userip { get; set; }

        public BmobBoolean isvip { get; set; }
        public BmobInt failedcount { get; set; }

        public BmobPointer<BmobUser> user { get; set; }

        public override void readFields(BmobInput input)
        {
            base.readFields(input);

            this.user = input.Get<BmobPointer<BmobUser>>("user");
            this.anonymity = input.getString("anonymity");
            this.anonymityen = input.getString("anonymityen");
            this.country = input.getString("country");
            this.countryen = input.getString("countryen");
            this.description = input.getString("description");
            this.failedcount = input.getInt("failedcount");
            this.isvip = input.getBoolean("isvip");
            this.port = input.getInt("port");
            this.proxy = input.getString("proxy");
            this.proxypassword = input.getString("proxypassword");
            this.proxyusername = input.getString("proxyusername");
            this.response = input.getInt("response");
            this.status = input.getInt("status");
            this.testdate = input.getString("testdate");
            this.type = input.getString("type");
            this.userip = input.getString("userip");
        }

        public override void write(BmobOutput output, Boolean all)
        {
            base.write(output, all);

            output.Put("anonymity", this.anonymity);
            output.Put("anonymityen", this.anonymityen);
            output.Put("country", this.country);
            output.Put("countryen", this.countryen);
            output.Put("description", this.description);
            output.Put("failedcount", this.failedcount);
            output.Put("isvip", this.isvip);
            output.Put("port", this.port);
            output.Put("proxy", this.proxy);
            output.Put("proxypassword", this.proxypassword);
            output.Put("proxyusername", this.proxyusername);
            output.Put("response", this.response);
            output.Put("status", this.status);
            output.Put("testdate", this.testdate);
            output.Put("type", this.type);
            output.Put("userip", this.userip);
            output.Put("user", this.user);
        }

        public ProxyServer Get()
        {
            var model = new ProxyServer();
            model.anonymity = this.anonymity;
            model.anonymityen = this.anonymityen;
            model.country = this.country;
            model.countryen = this.countryen;
            model.description = this.description;
            model.failedcount = this.failedcount.Get() ;
            model.isvip = this.isvip.Get()?1:0;
            model.port = this.port.Get();
            model.proxy = this.proxy ;
            model.proxypassword = this.proxypassword ;
            model.proxyusername = this.proxyusername;
            model.response = this.response.Get();
            model.status = this.status.Get();
            model.testdate = this.testdate;
            model.type = this.type;
            model.userip = this.userip ;

            return model;
        }

        public void Set(ProxyServer proxy)
        {
            this.anonymity = proxy.anonymity;
            this.anonymityen = proxy.anonymityen;
            this.country = proxy.country;
            this.countryen = proxy.countryen;
            this.description = proxy.description;
            this.failedcount = proxy.failedcount;
            this.isvip = proxy.isvip == 0 ? false : true;
            this.port = proxy.port;
            this.proxy = proxy.proxy;
            this.proxypassword = proxy.proxypassword;
            this.proxyusername = proxy.proxyusername;
            this.response = proxy.response;
            this.status = proxy.status;
            this.testdate = proxy.testdate;
            this.type = proxy.type;
            this.userip = proxy.userip;
        }
    }
}
