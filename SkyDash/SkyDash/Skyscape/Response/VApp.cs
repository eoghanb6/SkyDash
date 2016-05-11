using System.Collections.Generic;

namespace SkyDash.Skyscape.Response
{
    public class VApp
    {
        public int _id { get; set; }
        public string urn { get; set; }
        public string name { get; set; }
        public string ps { get; set; }
        public int total_vms { get; set; }
        public string month_to_date { get; set; }
        public string estimated_monthly_total { get; set; }

    }
}