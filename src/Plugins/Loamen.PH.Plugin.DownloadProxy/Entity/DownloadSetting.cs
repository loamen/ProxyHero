using System;
using System.Collections.Generic;

namespace Loamen.PH.Plugin.DownloadProxy.Entity
{
    [Serializable]
    public class DownloadSetting
    {
        private List<Website> websites = new List<Website>();

        public List<Website> Websites
        {
            get { return websites; }
            set { websites = value; }
        }
    }

    [Serializable]
    public class Website
    {
        public string Url { get; set; }
    }
}