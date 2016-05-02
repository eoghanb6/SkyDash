﻿using Newtonsoft.Json;

namespace Skydash.Response
{
    public class VirtualApp
    {
        [JsonProperty("_id")]
        public int Id { get; set; }

        [JsonProperty("urn")]
        public string Urn { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("ps")]
        public string PS { get; set; }

        [JsonProperty("total_vms")]
        public int TotalVms { get; set; }

        [JsonProperty("month_to_date")]
        public string MonthToDate { get; set; }

        [JsonProperty("estimated_monthly_total")]
        public string EstimatedMonthlyTotal { get; set; }
    }
}