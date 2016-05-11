using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyDash.Skyscape.Response
{
    public class VM
    {
        public int id { get; set; }
        public String urn { get; set; }
        public String name { get; set; }
        public String size { get; set; }
        public String ps { get; set; }
        public String os { get; set; }
        public String cpu { get; set; }
        public String mem { get; set; }
        public String storage { get; set; }
        public String created_at { get; set; }
        public String updated_at { get; set; }
        public String month_to_date { get; set; }
        public String estimated_mothly_total { get; set; }
        public String comment { get; set; }
        public String last_backup_status { get; set; }
        public String in_backup { get; set; }
        public String last_backup { get; set; }
        public String retention_length { get; set; }
        public String billed_hours_powered_on { get; set; }
        public String billed_hours_powered_off { get; set; }
        public Backup backups { get; set; }

    }
}