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
        public ActionResult Login()
        {

        }
        public ActionResult Index()
        {
            var authenticate = APIMethods.authenticate(Config.email, Config.password);
            var accounts = APIMethods.getAccounts(Config.email, Config.password);
            var vms = APIMethods.getVms(Config.email, Config.password);

            ViewBag.response = authenticate.Content;
            APIMethods.client.CookieContainer = new System.Net.CookieContainer();
                        
            //ViewBag.response2 = response2.Content;

            Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(accounts.Content);

            VM nicePrettyObject = JsonConvert.DeserializeObject<VM>(accounts.Content);
            ViewBag.response2 = nicePrettyObject;
            List<VM> listVms = new List<VM>();
            ViewBag.nicePrettyObject = values;

            Account fish = JsonConvert.DeserializeObject<Account>(vms.Content);
            ViewBag.response3 = fish.name;
                        
            //var thingsCostingMoreThan100 = vms.Where(x => x.Price > 100);

            //var totalCostPerMonth = vms.Sum(x => x.Price);

            // LINQ



            return View();
        }
    }
}