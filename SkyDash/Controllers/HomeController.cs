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
            viewModel.panelVMs = new List<PanelVM>();
            viewModel.accounts = new List<string>();
            int i = 0;

            if (authenticate.Content == "{\"expires_after\":900}")
            {
                ViewBag.response = "Authentication successful";
            

            var result = JsonConvert.DeserializeObject<Dictionary<string, VAppAndMachineWrapper>>(vms.Content, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy HH:mm" });

            foreach (var account in result)
            {
                var key = account.Key; // GOSH - Public
                    viewModel.accounts.Add(account.Key);

                    foreach (var vDC in account.Value.VirtualMachines)
                {
                    foreach (var vm in vDC)
                    {// e.g. "GOSH - Public (IL2-PROD-STANDARD)"

                      //  var brokenMachines = item.Value.Where(m => m.LastBackupStatus == "Failed");

                        foreach (var virtualMachine in vm.Value)
                        {
                            viewModel.panelVMs.Add(new PanelVM());
                            viewModel.panelVMs[i].Name = virtualMachine.Name;
                            viewModel.panelVMs[i].LastBackupStatus = virtualMachine.LastBackupStatus;
                            viewModel.panelVMs[i].LastBackup = virtualMachine.LastBackup;
                            i++;
                        }
                    }
                }

                foreach (var vm in account.Value.VDCs)
                {

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