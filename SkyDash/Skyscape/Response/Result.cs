using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyDash.Skyscape.Response
{
    public class Result
    {
        [JsonProperty("account")]
        public Account Account { get; set; }

        [JsonProperty("vOrgs")]
        public List<vOrg> vOrgs { get; set; }
    }
}