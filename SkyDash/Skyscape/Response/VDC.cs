using Newtonsoft.Json;
using Skydash.Response;
using System.Collections.Generic;

namespace SkyDash.Skyscape.Response
{
    public class vDC
    {
        //Gets and Sets vDC properties
        //JsonProperty used to link with properties in JSON string
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("urn")]
        public string Urn { get; set; }

        [JsonProperty("vApps")]
        public List<VirtualApp> vApps { get; set; }
    }
}