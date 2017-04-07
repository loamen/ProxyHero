namespace ProxyHero.Entity
{
    public class UserBbsEntity
    {
        private string contribution = "";
        private string copper = "";

        private string prestige = "";
        private string silver = "";
        private string vouchers = "";

        /// <summary>
        ///     铜币数量
        /// </summary>
        public string Copper
        {
            get { return copper; }
            set { copper = value; }
        }

        /// <summary>
        ///     威望
        /// </summary>
        public string Prestige
        {
            get { return prestige; }
            set { prestige = value; }
        }

        /// <summary>
        ///     贡献值
        /// </summary>
        public string Contribution
        {
            get { return contribution; }
            set { contribution = value; }
        }

        /// <summary>
        ///     银元
        /// </summary>
        public string Silver
        {
            get { return silver; }
            set { silver = value; }
        }

        /// <summary>
        ///     代理公布器使用券
        /// </summary>
        public string Vouchers
        {
            get { return vouchers; }
            set { vouchers = value; }
        }
    }
}