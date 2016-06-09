using Newtonsoft.Json;
using System.Collections.Generic;

namespace SkyDash.Skyscape.Response
{
    public class SkyscapeResponse
    {
        //Gets and Sets Response properties
        //JsonProperty used to link with properties in JSON string
        [JsonProperty("account")]
        public Account Account { get; set; }

        [JsonProperty("vOrgs")]
        public List<vOrg> vOrgs { get; set; }
    }
}