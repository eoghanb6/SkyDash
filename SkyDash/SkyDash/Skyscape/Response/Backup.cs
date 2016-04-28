using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyDash.Skyscape.Response
{
    public class Backup
    {
        public String status { get; set; }
        public String backup_slot { get; set; }
        public String backup_start { get; set; }
        public String backup_end { get; set; }
        public String snapshot_removal_start { get; set; }
        public String snapshot_removal_end { get; set; }
    }
}