using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loamen.Net.Entity
{
    public class TestResult
    {
        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsAlive { get; set; }

        /// <summary>
        /// 响应时间，单位：毫秒
        /// </summary>
        public long Response { get; set; }

        /// <summary>
        /// 错误信息或者HTML代码
        /// </summary>
        public string Message { get; set; }
    }
}
