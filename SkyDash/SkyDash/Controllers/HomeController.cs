using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSharp;
using Newtonsoft.Json;
using SkyDash.Skyscape.Response;
using SkyDash.Skyscape;

namespace SkyDash.Controllers
{
    public class HomeController : Controller
    {
      
        public ActionResult Index()
        {
            var authenticate = APIMethods.authenticate(Config.email, Config.password);
            var accounts = APIMethods.getAccounts(Config.email, Config.password);
            var vms = APIMethods.getVms(Config.email, Config.password);

            ViewBag.response = authenticate.Content;
            APIMethods.client.CookieContainer = new System.Net.CookieContainer();

            //ViewBag.response2 = response2.Content;

            //Account(VMs,Vapps)

            // VDCs (List of VDCs)>>VDC(List of VMs)>>VM(VM Details)>>Backup(Backup Details)


            // VDCs (List of VDCs)>>VDC(List of Vapps)>>Vapp(Vapp Details)
                        
            // LINQ



            return View();
        }
    }
}