using Loamen.Common;
using Loamen.Net;
using Loamen.Net.Entity;
using ProxyHero.Common;
using ProxyHero.Entity;
using ProxyHero.Properties;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ProxyHero.Net
{
    public class SnsHelper : HttpHelper
    {
        #region valiables

        private const string Encode = "UTF-8";
        private UserEntity _currentBbsUser;
        private UserEntity _currentUser;

        /// <summary>
        ///     当前主站用户
        /// </summary>
        public UserEntity CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }

        /// <summary>
        ///     当前论坛用户
        /// </summary>
        public UserEntity CurrentBbsUser
        {
            get { return _currentBbsUser; }
            set { _currentBbsUser = value; }
        }

        #endregion

        #region constructor

        public SnsHelper()
        {
            _currentUser = new UserEntity();
            _currentBbsUser = new UserEntity();
        }

        public SnsHelper(HttpOptions httpOption)
        {
            HttpOption = httpOption;
            _currentUser = new UserEntity();
            _currentBbsUser = new UserEntity();
        }

        #endregion

        #region 头像

        /// <summary>
        ///     用户头像路径
        /// </summary>
        public string AvatarPath
        {
            get
            {
                string userHeadPath = Config.MyDocumentsPath + "\\Loamen\\ProxyHero\\Faces\\" + _currentBbsUser.UserName +
                                      ".jpg";
                var fi = new FileInfo(userHeadPath);
                if (fi.Directory != null && !fi.Directory.Exists) fi.Directory.Create();

                return userHeadPath;
            }
        }

        /// <summary>
        ///     得到某个用户的头像
        /// </summary>
        /// <returns></returns>
        public Image Avatar
        {
            get
            {
                string userHeadPath = Config.MyDocumentsPath + "\\Loamen\\ProxyHero\\Faces\\" + _currentBbsUser.UserName +
                                      ".jpg";

                //判断头像是否存在
                var userHeadImage = File.Exists(userHeadPath) ? Image.FromFile(userHeadPath) : Resources.user;

                Image bmp = new Bitmap(userHeadImage);
                userHeadImage.Dispose();
                return bmp;
            }
        }

        #endregion

        #region mainSite

        /// <summary>
        ///     注册主站
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public object[] RegisterMainSite(UserEntity user)
        {
            var result = new object[] {false, "注册失败！"};
            try
            {
                const string mtype = "个人";
                var userid = user.UserName;
                var uname = user.UserName;
                var userpwd = user.PassWord;
                var userpwdok = user.PassWord;
                var email = user.Email;
                var sex = user.Sex;
                const string vdcode = "ProxyHeroReg";
                const string dopost = "regbase";
                const string step = "1";

                string postData = string.Format(
                    "mtype={0}&userid={1}&uname={2}&userpwd={3}&userpwdok={4}&email={5}&sex={6}&vdcode={7}&dopost={8}&step={9}",
                    mtype,
                    userid,
                    uname,
                    userpwd,
                    userpwdok,
                    email,
                    sex,
                    vdcode,
                    dopost,
                    step);

                var html = GetHtml("http://www.loamen.com/member/reg_new.php", postData, Encoding.GetEncoding(Encode));
                html = StringHelper.GetMidString(html, "<script>", "</script>");

                var regex = new Regex(@"document.write\(""(?<Result>.+)""");
                var matchsHtml = regex.Matches(html);

                if (html.Contains("注册成功"))
                {
                    _currentUser = user;
                    result[0] = true;
                }

                if (matchsHtml.Count == 4)
                {
                    result[1] = matchsHtml[2].Groups["Result"].Value;
                }

                result[0] = false;
                return result;
            }
            catch (Exception ex)
            {
                result[0] = false;
                result[1] = ex.Message;
                return result;
            }
        }

        /// <summary>
        ///     登陆主站
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public object[] LoginMainSite(UserEntity user)
        {
            var result = new object[] {false, "登录失败！"};
            try
            {
                const string fmdo = "login";
                const string dopost = "login";
                const string gourl = "";
                var userid = user.UserName;
                var pwd = user.PassWord;
                const string keeptime = "2592000";
                const string vdcode = "ProxyHeroLog";

                string postData = string.Format(
                    "fmdo={0}&dopost={1}&gourl={2}&userid={3}&pwd={4}&keeptime={5}&vdcode={6}",
                    fmdo,
                    dopost,
                    gourl,
                    userid,
                    pwd,
                    keeptime,
                    vdcode);

                var html = GetHtml("http://www.loamen.com/member/index_do.php", postData, Encoding.GetEncoding(Encode));

                if (html.Contains("成功登录"))
                {
                    _currentUser = user;
                    _currentUser.IsLogged = true;
                    result[0] = true;
                    result[1] = "成功登录！";
                    CookiesHelper.SetIECookie(this, "http://www.loamen.com");
                }
                else
                {
                    result[0] = false;
                }
            }
            catch (Exception ex)
            {
                result[0] = false;
                result[1] = ex.Message;
            }
            return result;
        }

        /// <summary>
        ///     注销主站
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public object[] LogoutMainSite(UserEntity user)
        {
            var result = new object[] {false, "退出失败！"};
            try
            {
                var html = GetHtml("http://www.loamen.com/member/index_do.php?fmdo=login&dopost=exit#", Encoding.GetEncoding(Encode));

                if (html.Contains("成功退出"))
                {
                    _currentUser = new UserEntity {IsLogged = false};
                    result[0] = true;
                    result[1] = "成功退出！";
                    CookiesHelper.DeleteCookie("http://www.loamen.com", "loamen");
                }
                else
                {
                    result[0] = false;
                }
            }
            catch (Exception ex)
            {
                result[0] = false;
                result[1] = ex.Message;
            }
            return result;
        }

        #endregion

        #region BBS

        /// <summary>
        ///     注册论坛
        /// </summary>
        /// <param name="user"></param>
        /// <param name="verifyCode"></param>
        /// <returns></returns>
        public object[] RegisterBbs(UserEntity user, string verifyCode)
        {
            var result = new object[] {false, "注册失败！"};
            try
            {
                string html = GetHtml("http://" + Config.ProxyHeroCloudSetting.BbsDomain + "/register.php");

                var regex = new Regex("document\\.register\\._hexie.value='(?<_hexie>\\w+)';");

                MatchCollection matchs = regex.Matches(html);
                if (matchs.Count > 0)
                {
                    const string forward = "";
                    const string step = "2";
                    var hexie = matchs[0].Groups["_hexie"].Value;
                    var regname = user.UserName;
                    var regpwd = user.PassWord;
                    var regpwdrepeat = user.PassWord;
                    var regemail = user.Email;
                    var regqkey = Config.ProxyHeroCloudSetting.RegQKey;
                    var gdcode = verifyCode;

                    regex = new Regex("正确答案:(?<regqkey>.+)</span>");
                    matchs = regex.Matches(html);
                    if (matchs.Count > 0)
                    {
                        regqkey = matchs[0].Groups["regqkey"].Value;
                    }

                    string postData = string.Format(
                        "forward={0}&step={1}&_hexie={2}&regname={3}&regpwd={4}&regpwdrepeat={5}&regemail={6}&qanswer={7}&qkey=0&rgpermit=1&regemailtoall=0&gdcode={8}",
                        forward,
                        step,
                        hexie,
                        regname,
                        regpwd,
                        regpwdrepeat,
                        regemail,
                        regqkey,
                        gdcode);

                    html = GetHtml("http://" + Config.ProxyHeroCloudSetting.BbsDomain + "/register.php?", postData, Encoding.GetEncoding(Encode));
                    result[1] = StringHelper.GetMidString(html, @"<span class=""f14"">", @"</span>");

                    if (html.Contains("注册成功"))
                    {
                        _currentBbsUser = user;
                        result[0] = true;
                        if (string.IsNullOrEmpty(result[1] + ""))
                        {
                            result[1] = "注册成功";
                        }
                    }
                    else
                    {
                        result[0] = false;
                    }
                }
                else if (html.Contains("提示信息"))
                {
                    result[1] = StringHelper.GetMidString(html, @"<span class=""f14"">", @"</span>");
                }
            }
            catch (Exception ex)
            {
                result[0] = false;
                result[1] = ex.Message;
            }

            return result;
        }

        /// <summary>
        ///     登录论坛
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public object[] LoginBbs(UserEntity user)
        {
            var result = new object[] {false, "登录失败！"};
            try
            {
                const string forward = "";
                var jumpurl = "http://" + Config.ProxyHeroCloudSetting.BbsDomain + "";
                const string step = "2";
                const string lgt = "0";
                var pwuser = user.UserName;
                var pwpwd = user.PassWord;
                const string hideid = "0";
                const string cktime = "2592000";

                string postData = string.Format(
                    "forward={0}&jumpurl={1}&step={2}&lgt={3}&pwuser={4}&pwpwd={5}&hideid={6}&cktime={7}",
                    forward,
                    jumpurl,
                    step,
                    lgt,
                    pwuser,
                    pwpwd,
                    hideid,
                    cktime);

                var html = GetHtml("http://" + Config.ProxyHeroCloudSetting.BbsDomain + "/login.php?",
                                                   postData, Encoding.GetEncoding(Encode));
                result[1] = StringHelper.GetMidString(html, @"<span class=""f14"">", @"</span>");

                if (html.Contains("顺利登录") || html.Contains("请不要重复登录"))
                {
                    _currentBbsUser = user;
                    _currentBbsUser.IsLogged = true;
                    result[0] = true;
                    CookiesHelper.SetIECookie(this, "http://" + Config.ProxyHeroCloudSetting.BbsDomain + "");

                    html = GetHtml("http://" + Config.ProxyHeroCloudSetting.BbsDomain + "/mode.php?m=o&q=user");
                    if (html.Contains("个人空间"))
                    {
                        //匹配头像url
                        if (!File.Exists(AvatarPath))
                        {
                            var regex =
                                new Regex(
                                    "<div\\sclass=\"user_face\"><img\\s+src=\"(?<url>[^\"]+)\"\\s+width=\"120\"\\s+height=\"120\"\\s+border=\"0");
                            MatchCollection matchs = regex.Matches(html);
                            var imagesHelper = new ImagesHelper();

                            if (matchs.Count > 0)
                            {
                                string imgesUrl = matchs[0].Groups["url"].Value;
                                imagesHelper.SaveImage(imgesUrl, AvatarPath, new Size(24, 24));
                            }
                            else
                            {
                                imagesHelper.SaveImage("http://bbs.uueasy.com/images/face/none.gif", AvatarPath,
                                                       new Size(24, 24));
                            }
                        }
                    }
                }
                else
                {
                    result[0] = false;
                }
            }
            catch (Exception ex)
            {
                result[0] = false;
                result[1] = ex.Message;
            }
            return result;
        }

        /// <summary>
        ///     注销论坛
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public object[] LogoutBbs(UserEntity user)
        {
            var result = new object[] {false, "退出失败！"};
            try
            {
                if (!_currentBbsUser.IsLogged)
                {
                    result[0] = true;
                    result[1] = "退出成功！";
                    return result;
                }

                string html = GetHtml("http://" + Config.ProxyHeroCloudSetting.BbsDomain + "/jobcenter.php");

                if (html.Contains("退出"))
                {
                    var regex = new Regex("<a\\s+href=\"(?<url>[^\"]+)\">退出");

                    MatchCollection matchs = regex.Matches(html);
                    if (matchs.Count > 0)
                    {
                        var exitUrl = matchs[0].Groups["url"].Value;

                        html = GetHtml("http://" + Config.ProxyHeroCloudSetting.BbsDomain + "/" + exitUrl, Encoding.GetEncoding(Encode));
                        result[1] = StringHelper.GetMidString(html, @"<span class=""f14"">", @"</span>");

                        if (html.Contains("顺利退出"))
                        {
                            _currentBbsUser = new UserEntity {IsLogged = false};
                            result[0] = true;
                            Cookies = null;
                            CookiesHelper.DeleteCookie("http://" + Config.ProxyHeroCloudSetting.BbsDomain + "", "loamen");
                        }
                        else
                        {
                            result[0] = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result[0] = false;
                result[1] = ex.Message;
            }
            return result;
        }

        /// <summary>
        ///     扣减积分
        /// </summary>
        /// <returns></returns>
        public object[] DeductPoints()
        {
            var result = new object[] {false, ""};
            if (CurrentBbsUser.IsLogged)
            {
                try
                {
                    string html =
                        GetHtml("http://" + Config.ProxyHeroCloudSetting.BbsDomain +
                                "/hack.php?H_name=toolcenter&action=mytool");
                    if (html.Contains("龙门代理公布器使用券"))
                    {
                        var regex =
                            new Regex(
                                "<a\\s+href=\"(?<url>[^\"]+)\"\\s+class=\"usetool\"\\s+onclick=\"return\\s+checkset\\('在使用龙门代理公布器");

                        MatchCollection matchs = regex.Matches(html);
                        if (matchs.Count > 0)
                        {
                            var toolUrl = matchs[0].Groups["url"].Value; //使用券道具地址

                            html = GetHtml("http://" + Config.ProxyHeroCloudSetting.BbsDomain + "/" + toolUrl, Encoding.GetEncoding(Encode));
                            result[1] = StringHelper.GetMidString(html, @"<span class=""f14"">", @"</span>");

                            if (html.Contains("贡献值"))
                            {
                                result[0] = true;
                            }
                            else
                            {
                                result[0] = false;
                            }
                        }
                        else
                        {
                            result[0] = false;
                            result[1] = "没有龙门代理公布器使用券，请上论坛道具中心免费领取！";
                        }
                    }
                    else
                    {
                        result[0] = false;
                        result[1] = "没有龙门代理公布器使用券，请上论坛道具中心免费领取！";
                    }
                }
                catch (Exception ex)
                {
                    result[0] = false;
                    result[1] = ex.Message;
                }
            }
            else
            {
                result[1] = "请先登录！";
            }

            return result;
        }

        public void GetUserInfo()
        {
            var url = "http://" + Config.ProxyHeroCloudSetting.BbsDomain +
                         "/hack.php?H_name=toolcenter&action=mytool";
            string html;
            if (Config.LocalSetting.IsUseSystemProxy)
            {
                IsUseDefaultProxy = true;
                html = GetHtml(url);
            }
            else
            {
                IsUseDefaultProxy = false;
                html = GetHtml(url);
            }

            if (html.Contains("我的道具箱"))
            {
                string propertyHtml = StringHelper.GetMidString(html, "等级:", "最后登录");
                var regex = new Regex(@"<li>铜币:\s+(?<Copper>\d+)\s+枚</li>");
                MatchCollection mathes = regex.Matches(propertyHtml);

                if (mathes.Count == 1)
                {
                    CurrentBbsUser.BbsProperties.Copper = mathes[0].Groups["Copper"].Value;
                }

                regex = new Regex(@"<li>威望:\s+(?<Prestige>\d+)\s+点</li>");
                mathes = regex.Matches(propertyHtml);
                if (mathes.Count == 1)
                {
                    CurrentBbsUser.BbsProperties.Prestige = mathes[0].Groups["Prestige"].Value;
                }

                regex = new Regex(@"<li>贡献值:\s+(?<Contribution>\S+)\s+点</li>");
                mathes = regex.Matches(propertyHtml);
                if (mathes.Count == 1)
                {
                    CurrentBbsUser.BbsProperties.Contribution = mathes[0].Groups["Contribution"].Value;
                }

                regex = new Regex(@"银元：</th><th>(?<Silver>\d+)\s+个");
                mathes = regex.Matches(html);
                if (mathes.Count == 1)
                {
                    CurrentBbsUser.BbsProperties.Silver = mathes[0].Groups["Silver"].Value;
                }

                regex = new Regex(@"用户名：</th><th><b>(?<UserName>\w+)</b>");
                mathes = regex.Matches(html);
                if (mathes.Count == 1)
                {
                    CurrentBbsUser.UserName = mathes[0].Groups["UserName"].Value;
                    CurrentBbsUser.NickName = mathes[0].Groups["UserName"].Value;
                }

                if (html.Contains("龙门代理公布器使用券"))
                {
                    propertyHtml = StringHelper.GetMidString(html, "<b>龙门代理公布器使用券</b>", "<!--");
                    CurrentBbsUser.BbsProperties.Vouchers = StringHelper.GetMidString(propertyHtml,
                                                                                      "拥有数量 <span style=\" color:#390;font-size:14px\"><b>",
                                                                                      "</b></span> 个</td>");
                }

                if (string.IsNullOrEmpty(CurrentBbsUser.BbsProperties.Vouchers))
                {
                    CurrentBbsUser.BbsProperties.Vouchers = "0";
                }

                var sb = new StringBuilder();
                sb.Append("铜币：" + _currentBbsUser.BbsProperties.Copper + "\n");
                sb.Append("威望：" + _currentBbsUser.BbsProperties.Prestige + "\n");
                sb.Append("贡献值：" + _currentBbsUser.BbsProperties.Contribution + "\n");
                sb.Append("银元：" + _currentBbsUser.BbsProperties.Silver + "\n");
                sb.Append("使用券：" + _currentBbsUser.BbsProperties.Vouchers);

                //Config.MainForm.toolTipExUserInfo.TitleText = "当前用户：" + currentBbsUser.UserName;
                //Config.MainForm.toolTipExUserInfo.MessageText = sb.ToString();

                _currentBbsUser.IsLogged = true;
            }
        }

        #endregion

        #region

        public Image GetVerifyCode()
        {
            var url = string.Format("http://" + Config.ProxyHeroCloudSetting.BbsDomain + "/ck.php?nowtime={0}",
                                       GetIntFromTime(DateTime.Now));
            return GetImage(url);
        }

        public long GetIntFromTime(DateTime dt)
        {
            const long lLeft = 621355968000000000;

            DateTime dt1 = dt.ToUniversalTime();
            long sticks = (dt1.Ticks - lLeft)/10000000;
            return sticks;
        }

        #endregion
    }
}