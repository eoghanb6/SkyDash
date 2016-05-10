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
using System.Threading;
using SkyDash.Skyscape.Response;

namespace SkyDash.Controllers
{
    public class HomeController : Controller
    {
        [OutputCache(Duration = 120)]
        public ActionResult Index()
        {
            var api = new APIMethods();

            var authenticate = api.authenticate(Config.email, Config.password);
            var accounts = api.getAccounts();
            
            VmViewModel viewModel = new VmViewModel();
            viewModel.names = new List<string>();
            viewModel.accountVms = new List<PanelVM>();
            viewModel.accounts = new List<Account>();
            int j = 0;

            if (authenticate.Content == "{\"expires_after\":900}")
            {
                ViewBag.response = "Authentication successful";

                viewModel.accounts = JsonConvert.DeserializeObject<List<Account>>(accounts.Content);
                for (int i = 0; i < viewModel.accounts.Count; i++)
                {
                    var vms = api.getVms(viewModel.accounts[i].id);
                    var result = JsonConvert.DeserializeObject<Result>(vms.Content, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy HH:mm" });

                    foreach (var vOrg in result.vOrgs)
                    {
                        foreach (var vDC in vOrg.VDCs)
                        {
                            foreach (var vApp in vDC.vApps)
                            {
                                //  var brokenMachines = item.Value.Where(m => m.LastBackupStatus == "Failed");

                                foreach (var virtualMachine in vApp.VMs)
                                {
                                    viewModel.accountVms.Add(new PanelVM());
                                    viewModel.accountVms[j].Name = virtualMachine.Name;
                                    viewModel.accountVms[j].LastBackupStatus = virtualMachine.LastBackupStatus;
                                    viewModel.accountVms[j].LastBackup = virtualMachine.LastBackup;
                                    viewModel.accountVms[j].AccountId = result.Account.id;
                                    j++;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                ViewBag.response = "Authentication Failed";
            }
            return View(viewModel);
        }
    }
}