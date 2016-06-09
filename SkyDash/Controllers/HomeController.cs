using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using SkyDash.Skyscape;
using Skydash.Response;
using SkyDash.ViewModels;
using Newtonsoft.Json.Converters;
using SkyDash.Skyscape.Response;
using SkyDash.vCloud.Response;
using System;
using System.IO;
using System.Xml.Serialization;

namespace SkyDash.Controllers
{
    public class HomeController : Controller
    {
        //Creates a cache of 240 seconds
        [OutputCache(Duration = 240)]
        //Action to check if authentication was successful
        public ActionResult PostLogin(string skyscapeUsername, string skyscapePassword)
        {

            var api = new APIMethods();
            //tries to authenticate with username and password entered in Login
            var authenticate = api.authenticateSkyscape(skyscapeUsername, skyscapePassword);
            //Checks for successful response from skyScape Portal API
            if (authenticate.Content == "{\"expires_after\":900}")
            {
                //if successful authentication, store user credentials as session variables 
                Session["SkyscapeUsername"] = skyscapeUsername;
                Session["SkyscapePassword"] = skyscapePassword;
                //redirect the user to the backups page
                return RedirectToAction("Backups", "Home");
            }
            else
            {
                //if authentication is not successful, redirect the user back to the login controller / view.
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult Logout()
        {
            //at logout remove session variables 
            Session.Remove("SkyscapeUsername");
            Session.Remove("SkyscapePassword");
            //redirect user to login controller / view
            return RedirectToAction("Login", "Home");
        }


        //Create the view for the Backups screen
        public ActionResult Backups()
        {
            //Set user credentials as session variables
            string username = Session["SkyscapeUsername"] as string;
            string password = Session["SkyscapePassword"] as string;

            //Initializes a new api call to Skyscape
            var api = new APIMethods();

            //Authentication details passed through from config class
            var authenticate = api.authenticateSkyscape(username, password);

            var accounts = api.getAccounts();

            //Generates viewModels for view
            VmViewModel vmViewModel = new VmViewModel();
            vmViewModel.names = new List<string>();
            vmViewModel.accountVms = new List<PanelVM>();
            vmViewModel.accounts = new List<Account>();
            vmViewModel.backups = new List<Backup>();

            // "expires_after : 900" means a successful authentication
            if (authenticate.Content == "{\"expires_after\":900}")
            {
                //Generates counter to uniquely identify JQuery dialogs in Backup view
                int k = 0;
                ViewBag.response = "Authentication successful";

                //Deserializes JSON string into account objects
                vmViewModel.accounts = JsonConvert.DeserializeObject<List<Account>>(accounts.Content);
                //Loop through deserialized accounts
                for (int i = 0; i < vmViewModel.accounts.Count; i++)
                {
                    //getVms makes the call to Skyscape to retrieve vm/backup information
                    var vms = api.getSkyscapeVms(vmViewModel.accounts[i].id);

                    //result is the objects of the returned deserialized JSON String from the API with a set DateTimeFormat included
                    var result = JsonConvert.DeserializeObject<SkyscapeResponse>(vms.Content, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy HH:mm" });

                    //To create VM objects, must loop through the different levels of JSON String
                    foreach (var vOrg in result.vOrgs)
                    {
                        foreach (var vDC in vOrg.vDCs)
                        {
                            foreach (var vApp in vDC.vApps)
                            {
                                foreach (var virtualMachine in vApp.VMs)
                                {
                                    //If any VM within an account has at a last backup which failed, count this fail to display in accordion view
                                    if (virtualMachine.LastBackupStatus.Contains("Failed"))
                                    {
                                        vmViewModel.accounts[i].allBackupsStatus = false;
                                        vmViewModel.accounts[i].numberFailedBackups++;
                                    }
                                    //create new instance of PanelVM
                                    PanelVM panelVm = new PanelVM();
                                    {
                                        panelVm.AccountId = result.Account.id;
                                        panelVm.Name = virtualMachine.Name;
                                        panelVm.LastBackupStatus = virtualMachine.LastBackupStatus;
                                        panelVm.LastBackup = virtualMachine.LastBackup;
                                        panelVm.backups = virtualMachine.Backups;
                                        panelVm.Id = virtualMachine.Id; //Not suitable as unique identifier for VM as may be duplicate in other accounts
                                        panelVm.Size = virtualMachine.Size;
                                        panelVm.MonthToDate = virtualMachine.MonthToDate;
                                        panelVm.EstimatedMonthlyTotal = virtualMachine.EstimatedMonthlyTotal;
                                        panelVm.BilledHoursPoweredOn = virtualMachine.BilledHoursPoweredOn;
                                        panelVm.BilledHoursPoweredOff = virtualMachine.BilledHoursPoweredOff;
                                        panelVm.PowerStatus = virtualMachine.PowerStatus;
                                        panelVm.OperatingSystem = virtualMachine.OperatingSystem;
                                        panelVm.NumberOfCPUs = virtualMachine.NumberOfCPUs;
                                        panelVm.Memory = virtualMachine.Memory;
                                        panelVm.Storage = virtualMachine.Storage;
                                        panelVm.counter = k++; //counter allows unique ID of VM without using any Skyscape data as key for VM
                                    }
                                    //Must add panelVm to view model in order to use in Backups view
                                    vmViewModel.accountVms.Add(panelVm);
                                }
                            }
                        }
                    }
                }
            }
            else //API Authentication has failed
            {
                ViewBag.response = "Authentication Failed";
                return RedirectToAction("Login", "Home");
            }
            return View(vmViewModel); // Must return viewModel to create view
        }

        //Runs when snapshots page is opened
        public ActionResult Snapshots()
        {
            string username = Session["SkyscapeUsername"] as string;
            string password = Session["SkyscapePassword"] as string;
            var api = new APIMethods();

            //Authentication details passed through from config class
            var authenticate = api.authenticateSkyscape(username, password);
            var accounts = api.getAccounts();

            SnapshotViewModel snapshotViewModel = new SnapshotViewModel();
            snapshotViewModel.skyscapeAccounts = new List<Account>();
            snapshotViewModel.Vms = new List<QueryResultRecords>();
            snapshotViewModel.vCloudAccounts = new Dictionary<int, Account>();
            int vmId = 0;

            //Deserializes JSON string into account objects
            snapshotViewModel.skyscapeAccounts = JsonConvert.DeserializeObject<List<Account>>(accounts.Content);

            //Loop through deserialized accounts
            foreach (Account account in snapshotViewModel.skyscapeAccounts)
            {
                Account vCloudAccount = new Account();
                {
                    vCloudAccount.vCloudCredentials = new List<Dictionary<string, vCloudIdentifiers>>();
                    vCloudAccount.vCloudToken = new Dictionary<string, int>();
                    var vCloudCredentialObject = (JsonConvert.DeserializeObject<Dictionary<string, vCloudIdentifiers>>(api.getVCloudCreds(account.id).Content));
                    vCloudAccount.id = account.id;
                    vCloudAccount.vCloudCredentials.Add(vCloudCredentialObject);
                    foreach (var vCloudCredential in vCloudAccount.vCloudCredentials)
                    {
                        foreach (var key in vCloudCredential.Keys)
                        {
                            if (key.Contains("-" + account.id.ToString() + "-"))
                            {
                                byte[] credentialsAsBytes = System.Text.Encoding.ASCII.GetBytes(vCloudCredential[key].username.ToString() + ":" + password);
                                string encodedCredentials = Convert.ToBase64String(credentialsAsBytes);
                                string token = api.authenticateVCloud(encodedCredentials);
                                if (token != null)
                                {
                                    vCloudAccount.vCloudToken.Add(token, account.id);
                                }
                            }
                        }
                    }
                }
                snapshotViewModel.vCloudAccounts.Add(account.id, vCloudAccount);
            }
            foreach (var vCloudAccount in snapshotViewModel.vCloudAccounts.Values)
            {
                foreach (string vCloudToken in vCloudAccount.vCloudToken.Keys)
                {
                    var vmsXml = api.getVCloudVms(vCloudToken).GetResponseStream();

                    //To create snapshot objects, must loop through the different levels of XML using a streamreader
                    var vmSerializer = new XmlSerializer(typeof(QueryResultRecords));
                    using (var vmStreamReader = new StreamReader(vmsXml))
                    {
                        QueryResultRecords Vms = (QueryResultRecords)vmSerializer.Deserialize(vmStreamReader);
                        Vms.vCloudId = vCloudAccount.id;
                        snapshotViewModel.Vms.Add(Vms);
                        foreach (var vm in Vms.VMRecord)
                        {
                            if (vm.CatalogName == null)
                            {
                                var snapshotXml = api.getVCloudVmsSnapshots(vm.Href, vCloudToken).GetResponseStream();
                                var snapshotSerializer = new XmlSerializer(typeof(SnapshotSection));
                                using (var snapshotStreamReader = new StreamReader(snapshotXml))
                                {
                                    SnapshotSection snapshot = (SnapshotSection)snapshotSerializer.Deserialize(snapshotStreamReader);
                                    if (snapshot.Snapshot != null)
                                    {
                                        vm.unofficialId = vmId;
                                        vmId++;
                                        vm.Snapshot = snapshot;
                                        vm.Snapshot.Snapshot.SizeInGB = long.Parse(vm.Snapshot.Snapshot.Size) / 1073741824;
                                        vm.Snapshot.Snapshot.accountId = vCloudAccount.vCloudToken[vCloudToken];
                                        if (vm.Snapshot.Snapshot.Created.AddDays(3) < DateTime.Now.Date)
                                        {
                                            foreach (var account in snapshotViewModel.skyscapeAccounts)
                                            {
                                                if (account.id == vCloudAccount.id)
                                                {
                                                    account.numberOldSnapshots++;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return View(snapshotViewModel);
        }

        public ActionResult Login()
        {
            LoginViewModel viewModel = new LoginViewModel();
            Session.Remove("SkyscapeUsername");
            Session.Remove("SkyscapePassword");
            return View(viewModel);
        }
    }

        internal class LoginViewModel
    {
    }
}