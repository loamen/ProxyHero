namespace ProxyHero.Entity
{
    public class UserEntity
    {
        private UserBbsEntity bbsProperties = new UserBbsEntity();
        private string email = "";
        private string nickName = "";
        private string passWord = "";
        private string sex = "";
        private string userName = "";

        /// <summary>
        ///     用户名
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        /// <summary>
        ///     密码
        /// </summary>
        public string PassWord
        {
            get { return passWord; }
            set { passWord = value; }
        }

        /// <summary>
        ///     邮箱
        /// </summary>
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        /// <summary>
        ///     昵称
        /// </summary>
        public string NickName
        {
            get { return nickName; }
            set { nickName = value; }
        }

        /// <summary>
        ///     性别
        /// </summary>
        public string Sex
        {
            get { return sex; }
            set { sex = value; }
        }

        /// <summary>
        ///     是否登录
        /// </summary>
        public bool IsLogged { get; set; }

        /// <summary>
        ///     论坛属性
        /// </summary>
        public UserBbsEntity BbsProperties
        {
            get { return bbsProperties; }
            set { bbsProperties = value; }
        }
    }
}