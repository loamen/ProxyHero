using cn.bmob.api;
using cn.bmob.json;
using cn.bmob.tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProxyHero
{
    public partial class BmobBaseForm : Form
    {
        //创建Bmob实例
        private BmobWindows bmob;
        public BmobBaseForm() : base()
        {
            InitializeComponent();

            bmob = new BmobWindows();

            //初始化ApplicationId，这个ApplicationId需要更改为你自己的ApplicationId（ http://www.bmob.cn 上注册登录之后，创建应用可获取到ApplicationId）
            Bmob.initialize("5fcf8cca1113b5136fca2007de03892d", "");

            //注册调试工具
            BmobDebug.Register(msg => { Debug.WriteLine(msg); });
        }

        public BmobWindows Bmob
        {
            get { return bmob; }
        }

        //对返回结果进行显示处理
        public void FinishedCallback<T>(T data, TextBox text)
        {
            text.Text = JsonAdapter.JSON.ToDebugJsonString(data);
        }

    }
}
