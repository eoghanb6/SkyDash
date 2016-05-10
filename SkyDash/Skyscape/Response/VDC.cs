using Newtonsoft.Json;
using Skydash.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyDash.Skyscape.Response
{
    public class VDC
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("urn")]
        public string Urn { get; set; }

        [JsonProperty("vApps")]
        public List<VirtualApp> vApps { get; set; }
    }
}