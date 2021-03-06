﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace SkyDash.Skyscape.Response
{
    public class Account
    {
        //uses Json property to link to JSON string
        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("id")]
        public int id { get; set; }

        //initialize variables to be changed within controller
        public bool allBackupsStatus = true;
        public int numberFailedBackups = 0;
        public int numberOldSnapshots = 0;
        public Dictionary<string, int> vCloudToken;
        public List<Dictionary<string, vCloudIdentifiers>> vCloudCredentials;
        public int serviceId;
        public bool hasAccess = true;

    }
}