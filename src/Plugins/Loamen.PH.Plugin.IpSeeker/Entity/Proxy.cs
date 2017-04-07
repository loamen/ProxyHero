using System;
using System.Collections.Generic;
using System.Text;

namespace Loamen.PH.Plugin.IpSeeker.Entity
{
    [Serializable]
    public class Proxy
    {
        public Proxy()
        { }
        public Proxy(string ip, int port)
        {
            this.Ip = ip;
            this.Port = port;
        }
        public string Ip { get; set; }
        public int Port { get; set; }
        public string IpAndPort
        {
            get { return this.Ip + ":" + this.Port; }
        }
    }
}