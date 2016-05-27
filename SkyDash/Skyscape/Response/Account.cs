using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyDash.Skyscape.Response
{
    public class Account
    {
        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("id")]
        public int id { get; set; }

        //initialize variables to be changed within controller
        public bool allBackupsStatus = true;
        public int numberFailedBackups = 0;
    }
}