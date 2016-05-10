using Newtonsoft.Json;
using Skydash.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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