using cn.bmob.io;

namespace ProxyHero.Entity
{
    public class UserEntity : BmobUser
    {
        /// <summary>
        ///     昵称
        /// </summary>
        public string NickName
        {
            get;set;
        }

        /// <summary>
        ///     性别
        /// </summary>
        public BmobBoolean Sex
        {
            get;set;
        }

        public string avatar { get; set; }

        /// <summary>
        ///  微信编号
        /// </summary>
        public string WechatOpenId { get; set; }

        //读字段信息
        public override void readFields(BmobInput input)
        {
            base.readFields(input);

            this.NickName = input.getString("NickName");
            this.Sex = input.getBoolean("Sex");
            this.WechatOpenId = input.getString("WechatOpenId");
            this.avatar = input.getString("avatar");
        }

        //写字段信息
        public override void write(BmobOutput output, bool all)
        {
            base.write(output, all);

            output.Put("NickName", this.NickName);
            output.Put("Sex", this.Sex);
            output.Put("WechatOpenId", this.WechatOpenId);
            output.Put("avatar", this.avatar);
        }
    }
}