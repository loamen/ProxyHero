using System;

namespace Loamen.PH.Plugin.IpSeeker
{
    [Serializable]
    public class IpSeekerSetting
    {
        public string DbFileName { get; set; }
        public int ThreadCount { get; set; }
    }
}