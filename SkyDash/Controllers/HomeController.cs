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

            ViewBag.response = authenticate.Content;
            

            var result = JsonConvert.DeserializeObject<Dictionary<String, VAppAndMachineWrapper>>(vms.Content);

            ViewBag.response2 = result;


            foreach (var account in result)
            {
                var key = account.Key; // GOSH - Public

                foreach (var vm in account.Value.VirtualMachines)
                {
                    foreach (var item in vm)
                    {
                        var x = item.Key; // e.g. "GOSH - Public (IL2-PROD-STANDARD)"

                        var brokenMachines = item.Value.Where(m => m.LastBackupStatus == "bannas");

                        foreach (var virtualMachine in item.Value)
                        {
                            virtualMachine.
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

            VmViewModel viewModel = new VmViewModel();
            // viewModel.backups = 

            return View(viewModel);
        }
    }
}