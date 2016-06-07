using Newtonsoft.Json;
using System.Collections.Generic;

namespace SkyDash.Skyscape.Response
{
    public class SkyscapeResponse
    {
        [JsonProperty("account")]
        public Account Account { get; set; }

        [JsonProperty("vOrgs")]
        public List<vOrg> vOrgs { get; set; }
    }
}