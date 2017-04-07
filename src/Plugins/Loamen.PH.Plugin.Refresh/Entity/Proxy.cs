using System;
using System.Collections.Generic;
using System.Text;

namespace Loamen.RefreshPlugin.Entity
{
    [Serializable]
    public class Proxy
    {
        public Proxy(string ip, int port)
        {
            this.IP = ip;
            this.Port = port;
        }
        public string IP { get; set; }
        public int Port { get; set; }
        public string IpAndPort
        {
            get { return this.IP + ":" + this.Port; }
        }
    }
}
