using cn.bmob.io;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyHero.Entity
{
    public class OperatorLog : BmobTable
    {
        //对应要操作的数据表
        public const String TABLE_NAME = "OperatorLogs";
        private String fTable;

        public OperatorLog() :this(TABLE_NAME)
        {
        }

        public OperatorLog(String tableName)
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

        public string title { get; set; }
        public string message { get; set; }
        public string type { get; set; }
        public string contact { get; set; }
        public string userip { get; set; }
        public BmobPointer<BmobUser> user { get; set; }

        public override void readFields(BmobInput input)
        {
            base.readFields(input);

            this.user = input.Get<BmobPointer<BmobUser>>("user");
            this.title = input.getString("title");
            this.message = input.getString("message");
            this.type = input.getString("type");
            this.contact = input.getString("contact");
            this.userip = input.getString("userip");
        }

        public override void write(BmobOutput output, Boolean all)
        {
            base.write(output, all);

            output.Put("title", this.title);
            output.Put("message", this.message);
            output.Put("type", this.type);
            output.Put("contact", this.contact);
            output.Put("userip", this.userip);
            output.Put("user", this.user);
        }
    }
}
