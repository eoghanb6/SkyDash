using Newtonsoft.Json;
using System.Collections.Generic;

namespace SkyDash.Skyscape.Response
{
    public class vOrg
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("serviceId")]
        public string ServiceId { get; set; }

        [JsonProperty("urn")]
        public string Urn { get; set; }

        [JsonProperty("VDCs")]
        public List<VDC> VDCs { get; set; }
    }
}