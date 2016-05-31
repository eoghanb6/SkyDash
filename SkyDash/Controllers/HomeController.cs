using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using SkyDash.Skyscape;
using Skydash.Response;
using SkyDash.ViewModels;
using Newtonsoft.Json.Converters;
using SkyDash.Skyscape.Response;

namespace SkyDash.Controllers
{
    public class HomeController : Controller
    {
        //Creates a cache of 240 seconds
        [OutputCache(Duration = 240)]

        public ActionResult PostLogin(string skyscapeUsername, string skyscapePassword) {
            
            var api = new APIMethods();
            var authenticate = api.authenticate(skyscapeUsername, skyscapePassword);
            if (authenticate.Content == "{\"expires_after\":900}")
            {
                
                Session["SkyscapeUsername"] = skyscapeUsername;
                Session["SkyscapePassword"] = skyscapePassword;
                return RedirectToAction("Backups", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult Logout() {
            Session.Remove("SkyscapeUsername");
            Session.Remove("SkyscapePassword");
            return RedirectToAction("Login", "Home");
        }


        //Create the view for the Backups screen
        public ActionResult Backups()
        {
            string username = Session["SkyscapeUsername"] as string;
            string password = Session["SkyscapePassword"] as string;

            //Initializes a new api call to Skyscape
            var api = new APIMethods();

            //Authentication details passed through from config class
            var authenticate = api.authenticate(username, password);
            var accounts = api.getAccounts();

            //Generates viewModels for view
            VmViewModel viewModel = new VmViewModel();
            viewModel.names = new List<string>();
            viewModel.accountVms = new List<PanelVM>();
            viewModel.accounts = new List<Account>();
            viewModel.backups = new List<Backup>();

            // "expires_after : 900" means a successful authentication
            if (authenticate.Content == "{\"expires_after\":900}")
            {
                //Generates counter to uniquely identify JQuery dialogs in Backup view
                int k = 0;
                ViewBag.response = "Authentication successful";
                
                //Deserializes JSON string into account objects
                viewModel.accounts = JsonConvert.DeserializeObject<List<Account>>(accounts.Content);
                //Loop through deserialized accounts
                for (int i = 0; i < viewModel.accounts.Count; i++)
                { 
                    //getVms makes the call to Skyscape to retrieve vm/backup information
                    var vms = api.getVms(viewModel.accounts[i].id);
                    //result is the objects of the returned deserialized JSON String from the API with a set DateTimeFormat included
                    var result = JsonConvert.DeserializeObject<Result>(vms.Content, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy HH:mm" });

                    //To create VM objects, must loop through the different levels of JSON String
                    foreach (var vOrg in result.vOrgs)
                    {
                        foreach (var vDC in vOrg.VDCs)
                        {                            
                            foreach (var vApp in vDC.vApps)
                            {
                                foreach (var virtualMachine in vApp.VMs)
                                {
                                    //If any VM within an account has at a last backup which failed, count this fail to display in accordion view
                                    if (virtualMachine.LastBackupStatus.Contains("Failed")) {
                                        viewModel.accounts[i].allBackupsStatus = false;
                                        viewModel.accounts[i].numberFailedBackups++;
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
                                    viewModel.accountVms.Add(panelVm);
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
            return View(viewModel); // Must return viewModel to create view
        }

        [OutputCache(Duration = 180)]
        public ActionResult Snapshots()
        {
            SnapshotViewModel viewModel = new SnapshotViewModel();
            return View(viewModel);
        }

        [OutputCache(Duration = 180)]
        public ActionResult Login()
        {
            LoginViewModel viewModel = new LoginViewModel();
            Session.Remove("SkyscapeUsername");
            Session.Remove("SkyscapePassword");
            return View(viewModel);
        }
    }
}