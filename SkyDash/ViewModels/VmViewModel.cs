using Skydash.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyDash.ViewModels
{
    public class VmViewModel
    {
        public List<string> accounts { get; set; }
        public List<string> names { get; set; }
        public string last_backup_status { get; set; }
        public List<Backup> backups { get; set; }
        public List<PanelVM> panelVMs { get; set; }
    }
}