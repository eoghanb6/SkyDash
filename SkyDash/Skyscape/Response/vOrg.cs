using Newtonsoft.Json;
using System.Collections.Generic;

namespace SkyDash.Skyscape.Response
{
    public class vOrg
    {
        //Gets and Sets vOrg properties
        //JsonProperty used to link with properties in JSON string
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("serviceId")]
        public string ServiceId { get; set; }

        [JsonProperty("urn")]
        public string Urn { get; set; }

        [JsonProperty("VDCs")]
        public List<vDC> vDCs { get; set; }
    }
}