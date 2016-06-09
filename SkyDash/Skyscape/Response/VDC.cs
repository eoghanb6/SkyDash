using Newtonsoft.Json;
using Skydash.Response;
using System.Collections.Generic;

namespace SkyDash.Skyscape.Response
{
    public class vDC
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("urn")]
        public string Urn { get; set; }

        [JsonProperty("vApps")]
        public List<VirtualApp> vApps { get; set; }
    }
}