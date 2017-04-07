using System.Text;
using System.Windows.Forms;

namespace ProxyHero.Net
{
    public class UpdateHelper
    {
        public string LocalVersion = "";
        public string NewVersion = "";

        /// <summary>
        ///     检测版本是否需要更新
        /// </summary>
        /// <returns></returns>
        public string CheckVersion()
        {
            var sb = new StringBuilder("");
            NewVersion = Config.ProxyHeroCloudSetting.Version;
            LocalVersion = Application.ProductVersion;

            if (string.IsNullOrEmpty(NewVersion))
                return "";

            double dbNewVersion = double.Parse(NewVersion.Replace(".", ""));
            double dbLocalVersion = double.Parse(LocalVersion.Replace(".", ""));

            if (dbNewVersion > dbLocalVersion)
            {
                sb.Append("服务器版本：");
                sb.Append(NewVersion);
                sb.Append("\n");
                sb.Append("  当前版本：");
                sb.Append(LocalVersion);
                sb.Append("\n");
                sb.Append("是否立即更新？");
            }
            else
            {
                sb.Append("");
            }

            return sb.ToString();
        }
    }
}