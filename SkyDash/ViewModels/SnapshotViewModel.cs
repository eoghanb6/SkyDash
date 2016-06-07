using SkyDash.Skyscape.Response;
using SkyDash.vCloud.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static SkyDash.vCloud.Response.VMRecord;

namespace SkyDash.ViewModels
{
    internal class SnapshotViewModel
    {
        public List<Account> skyscapeAccounts { get; set; }
        public Dictionary<int, Account> vCloudAccounts { get; set; }
        public List<QueryResultRecords> Vms { get; set; }
    }
}