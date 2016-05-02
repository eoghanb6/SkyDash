using SkyDash.Skyscape.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyDash.ViewModels
{
    public class VmViewModel
    {
        public int id { get; set; }
        public String name { get; set; }
        public String last_backup_status { get; set; }
        public Backup backups { get; set; }

    }
}