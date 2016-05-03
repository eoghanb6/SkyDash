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

            if (authenticate.Content == "{\"expires_after\":900}")
            {
                ViewBag.response = "Authentication successful";
            }
            else
            {
                ViewBag.response = "Authentication Failed";
            }

            var result = JsonConvert.DeserializeObject<Dictionary<string, VAppAndMachineWrapper>>(vms.Content, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy HH:mm" });

            foreach (var account in result)
            {
                var key = account.Key; // GOSH - Public

                foreach (var vDC in account.Value.VirtualMachines)
                {
                    foreach (var vm in vDC)
                    {// e.g. "GOSH - Public (IL2-PROD-STANDARD)"

                      //  var brokenMachines = item.Value.Where(m => m.LastBackupStatus == "bannas");

                        foreach (var virtualMachine in vm.Value)
                        {
                            viewModel.names.Add(virtualMachine.Name);
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
                     
            return View(viewModel);
        }
    }
}