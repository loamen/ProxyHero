using System.Text;
using System.Windows.Forms;

namespace ProxyHero.Entity
{
    /*
<?xml version="1.0" encoding="utf-8"?>
<ProxyHeroEntity xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Domain>http://www.loamen.com/soft/proxyhero/</Domain>
  <CommercialUrl>http://weibo.com/loamen</CommercialUrl>
  <EnglishCommercialUrl>http://www.loamen.com/</EnglishCommercialUrl>
  <StatisticsUrl>http://www.loamen.com/mini/</StatisticsUrl>
  <EnglishStatisticsUrl>http://www.loamen.com/mini/</EnglishStatisticsUrl>
  <EnableCommercialPage>0</EnableCommercialPage>
  <Version>1.6.2</Version>
  <UpdatedEnableUse>1</UpdatedEnableUse>
  <EnableDeductPoints>0</EnableDeductPoints>
  <CloudTestHours>1</CloudTestHours>
  <BbsDomain>https://github.com/loamen/ProxyHero/issues</BbsDomain>
  <UpdateUrl>http://www.loamen.com</UpdateUrl>
  <About>Loamen Proxy Hero is a professional proxy software to test, use and manage proxy list. </About>
</ProxyHeroEntity>
    */

    public class ProxyHeroEntity
    {
        private string about;
        private string bbsDomain = "bbs.loamen.com";
        private int cloudTestHours = 12;
        private string commercialUrl = "http://www.loamen.com/";
        private string enableCommercialPage = "0";
        private string enableDeductPoints = "0";

        private string englishCommercialUrl = "http://en.loamen.com/";
        private string englishStatisticsUrl = "http://en.loamen.com/mini/";
        private string statisticsUrl = "http://www.loamen.com/mini/";
        private string updateUrl = "http://www.loamen.com";
        private string updatedEnableUse = "1";
        private string version = "";

        /// <summary>
        ///     弹出广告网站
        /// </summary>
        public string CommercialUrl
        {
            get { return commercialUrl; }
            set { commercialUrl = value; }
        }

        /// <summary>
        ///     英文版广告网站
        /// </summary>
        public string EnglishCommercialUrl
        {
            get { return englishCommercialUrl; }
            set { englishCommercialUrl = value; }
        }

        /// <summary>
        ///     是否显示广告网页，0不显示；1显示
        /// </summary>
        public string EnableCommercialPage
        {
            get { return enableCommercialPage; }
            set { enableCommercialPage = value; }
        }

        /// <summary>
        ///     统计页面网址,起始页
        /// </summary>
        public string StatisticsUrl
        {
            get
            {
                if (statisticsUrl.Contains("?"))
                {
                    statisticsUrl = statisticsUrl + "&n=ProxyHero&v=" + Application.ProductVersion;
                }
                else
                {
                    statisticsUrl = statisticsUrl + "?n=ProxyHero&v=" + Application.ProductVersion;
                }
                return statisticsUrl;
            }
            set { statisticsUrl = value; }
        }

        /// <summary>
        ///     英文版统计页面网址,起始页
        /// </summary>
        public string EnglishStatisticsUrl
        {
            get { return englishStatisticsUrl; }
            set { englishStatisticsUrl = value; }
        }

        /// <summary>
        ///     服务器最新版本
        /// </summary>
        public string Version
        {
            get { return version; }
            set { version = value; }
        }

        /// <summary>
        ///     升级后旧版本程序是否可用
        /// </summary>
        public string UpdatedEnableUse
        {
            get { return updatedEnableUse; }
            set { updatedEnableUse = value; }
        }

        /// <summary>
        ///     是否允许扣减积分，0不扣减；1扣减
        /// </summary>
        public string EnableDeductPoints
        {
            get { return enableDeductPoints; }
            set { enableDeductPoints = value; }
        }

        /// <summary>
        ///     云引擎验证时长；在这范围内全部验证时不验证。
        /// </summary>
        public int CloudTestHours
        {
            get { return cloudTestHours; }
            set { cloudTestHours = value; }
        }

        /// <summary>
        ///     论坛域名
        /// </summary>
        public string BbsDomain
        {
            get { return bbsDomain; }
            set { bbsDomain = value; }
        }

        /// <summary>
        ///     下载新版本链接
        /// </summary>
        public string UpdateUrl
        {
            get { return updateUrl; }
            set { updateUrl = value; }
        }

        /// <summary>
        ///     关于的内容说明
        /// </summary>
        public string About
        {
            get
            {
                if (string.IsNullOrEmpty(about))
                {
                    var sb = new StringBuilder();
                    sb.Append("龙门中文官方网站：http://www.loamen.com\n");
                    sb.Append("LPH English Homepage: http://en.loamen.com\n");
                    sb.Append("Email：loamen.com@gmail.com\n");
                    about = sb.ToString();
                }
                return about;
            }
            set { about = value; }
        }
    }
}