using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSharp;
using Newtonsoft.Json;
using SkyDash.Skyscape;
using Skydash.Response;
using System.IO;
using RestSharp.Deserializers;
using SkyDash.ViewModels;
using Newtonsoft.Json.Converters;

namespace SkyDash.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var api = new APIMethods();

            var authenticate = api.authenticate(Config.email, Config.password);
            var accounts = api.getAccounts();
            var vms = api.getVms();
            VmViewModel viewModel = new VmViewModel();
            viewModel.names = new List<string>();
            viewModel.account1Vms = new List<PanelVM>();
            viewModel.account2Vms = new List<PanelVM>();
            viewModel.accounts = new List<string>();
            int i = 0;
            int j = 0;

            if (authenticate.Content == "{\"expires_after\":900}")
            {
                ViewBag.response = "Authentication successful";

            var result = JsonConvert.DeserializeObject<Dictionary<string, VAppAndMachineWrapper>>(vms.Content, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy HH:mm" });

                foreach (var account in result) {
                    viewModel.accounts.Add(account.Key);
                }

                foreach (var vDC in result[viewModel.accounts[0]].VirtualMachines)
                {
                    foreach (var vm in vDC)
                    {
                        //  var brokenMachines = item.Value.Where(m => m.LastBackupStatus == "Failed");

                        foreach (var virtualMachine in vm.Value)
                        {
                            if (vm.Key.Contains(viewModel.accounts[0].Substring(0, 4)))
                            {
                                viewModel.account1Vms.Add(new PanelVM());
                                viewModel.account1Vms[i].Name = virtualMachine.Name;
                                viewModel.account1Vms[i].LastBackupStatus = virtualMachine.LastBackupStatus;
                                viewModel.account1Vms[i].LastBackup = virtualMachine.LastBackup;
                                i++;
                            }
                            else if (vm.Key.Contains(viewModel.accounts[1].Substring(0, 5)))
                            {
                                viewModel.account2Vms.Add(new PanelVM());
                                viewModel.account2Vms[j].Name = virtualMachine.Name;
                                viewModel.account2Vms[j].LastBackupStatus = virtualMachine.LastBackupStatus;
                                viewModel.account2Vms[j].LastBackup = virtualMachine.LastBackup;
                                j++;
                            }
                        }
                    }
                }
            

                //Account(VMs,Vapps)

                // VDCs (List of VDCs)>>VDC(List of VMs)>>VM(VM Details)>>Backup(Backup Details)

                // VDCs (List of VDCs)>>VDC(List of Vapps)>>Vapp(Vapp Details)

                // LINQ 
            }
            else
            {
                ViewBag.response = "Authentication Failed";
            }
                     
            return View(viewModel);

           
        }
    }
}