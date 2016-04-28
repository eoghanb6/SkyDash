using System.Collections.Generic;

namespace SkyDash.Skyscape.Response
{
    public class VApp
    {
        public Dictionary<string, VM> Items { get; set; }

        //[Json("vapps")]
        public List<VApp> VirtualApps { get; set; }
    }
}