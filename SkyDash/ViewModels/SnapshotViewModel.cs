using SkyDash.Skyscape.Response;
using SkyDash.vCloud.Response;
using System.Collections.Generic;

namespace SkyDash.ViewModels
{
    //Snapshot view model for information within snapshot view
    public class SnapshotViewModel
    {
        public List<Account> skyscapeAccounts { get; set; }
        public Dictionary<int, Account> vCloudAccounts { get; set; }
        public List<QueryResultRecords> Vms { get; set; }
    }
}