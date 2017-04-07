using System;
using System.Drawing;
using Loamen.Net;

namespace ProxyHero.Common
{
    public class ImagesHelper : HttpHelper
    {
        /// <summary>
        ///     获取灰度图片
        /// </summary>
        /// <param name="MyBitmap"></param>
        /// <param name="Width">图片宽度</param>
        /// <param name="Height">图片高度</param>
        /// <returns></returns>
        public Bitmap GetDarkPicture(Bitmap Bitmap, int Width, int Height)
        {
            var bitmap = new Bitmap(Width, Height);
            Color pixel;
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    pixel = Bitmap.GetPixel(x, y);
                    int r, g, b, Result = 0;
                    r = pixel.R;
                    g = pixel.G;
                    b = pixel.B;
                    //实例程序以加权平均值法产生黑白图像
                    int iType = 2;
                    switch (iType)
                    {
                        case 0: //平均值法
                            Result = ((r + g + b)/3);
                            break;
                        case 1: //最大值法
                            Result = r > g ? r : g;
                            Result = Result > b ? Result : b;
                            break;
                        case 2: //加权平均值法
                            Result = ((int) (0.7*r) + (int) (0.2*g) + (int) (0.1*b));
                            break;
                    }
                    bitmap.SetPixel(x, y, Color.FromArgb(Result, Result, Result));
                }
            return bitmap;
        }

        /// <summary>
        ///     获取灰度图片
        /// </summary>
        /// <param name="FileName">图片文件名称</param>
        /// <param name="Width">图片宽度</param>
        /// <param name="Height">图片高度</param>
        /// <returns></returns>
        public Bitmap GetDarkPicture(string FileName, int Width, int Height)
        {
            var bitmap = new Bitmap(Width, Height);
            var Bitmap = new Bitmap(FileName);
            Color pixel;
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    pixel = Bitmap.GetPixel(x, y);
                    int r, g, b, Result = 0;
                    r = pixel.R;
                    g = pixel.G;
                    b = pixel.B;
                    //实例程序以加权平均值法产生黑白图像
                    int iType = 2;
                    switch (iType)
                    {
                        case 0: //平均值法
                            Result = ((r + g + b)/3);
                            break;
                        case 1: //最大值法
                            Result = r > g ? r : g;
                            Result = Result > b ? Result : b;
                            break;
                        case 2: //加权平均值法
                            Result = ((int) (0.7*r) + (int) (0.2*g) + (int) (0.1*b));
                            break;
                    }
                    bitmap.SetPixel(x, y, Color.FromArgb(Result, Result, Result));
                }
            return bitmap;
        }

        /// <summary>
        ///     保存远程图片到本地
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public string SaveImage(string Url, string FileName, Size PicSize)
        {
            try
            {
                Image image = GetImage(Url);
                image = new Bitmap(image, PicSize);
                image.Save(FileName);
                return FileName;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string SaveImage(string Url, string FileName)
        {
            try
            {
                Image image = GetImage(Url);
                image = new Bitmap(image);
                image.Save(FileName);
                return FileName;
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        ///     保存远程图片到本地
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public Bitmap SaveBitmap(string Url, string FileName)
        {
            try
            {
                var bitMap = new Bitmap(GetImage(Url));
                bitMap.Save(FileName);

                return bitMap;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}