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

            ViewBag.response = authenticate.Content;
            

            var result = JsonConvert.DeserializeObject<Dictionary<String, VAppAndMachineWrapper>>(vms.Content, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy HH:mm" });


            ViewBag.response2 = result;
            VmViewModel viewModel = new VmViewModel();

            foreach (var account in result)
            {
                var key = account.Key; // GOSH - Public

                foreach (var vm in account.Value.VirtualMachines)
                {
                    foreach (var item in vm)
                    {
                        var x = item.Key; // e.g. "GOSH - Public (IL2-PROD-STANDARD)"

                      //  var brokenMachines = item.Value.Where(m => m.LastBackupStatus == "bannas");

                        foreach (var virtualMachine in item.Value)
                        {
                            viewModel.ids.Add(virtualMachine.Id);
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

            
            // viewModel.backups = 

            return View(viewModel);
        }
    }
}