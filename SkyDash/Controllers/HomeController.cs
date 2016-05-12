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
            viewModel.backups = new List<Backup>();

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
                                foreach (var virtualMachine in vApp.VMs)
                                {
                                    PanelVM panelVm = new PanelVM();
                                    {
                                        panelVm.AccountId = result.Account.id;
                                        panelVm.Name = virtualMachine.Name;
                                        panelVm.LastBackupStatus = virtualMachine.LastBackupStatus;
                                        panelVm.LastBackup = virtualMachine.LastBackup;
                                        panelVm.backups = virtualMachine.Backups;
                                    }
                                    viewModel.accountVms.Add(panelVm);
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