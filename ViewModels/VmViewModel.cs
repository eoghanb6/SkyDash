using Skydash.Response;
using SkyDash.Skyscape.Response;
using System.Collections.Generic;

namespace SkyDash.ViewModels
{
    public class VmViewModel
    {
        public List<Account> accounts { get; set; }
        public List<string> names { get; set; }
        public string last_backup_status { get; set; }
        public List<Backup> backups { get; set; }
        public List<PanelVM> accountVms { get; set; }
        public Result result { get; set; }
    }
}