using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSharp;
using Newtonsoft.Json;
using SkyDash.Skyscape.Response;

namespace SkyDash.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var client = new RestClient("https://portal.skyscapecloud.com/api");
            client.CookieContainer = new System.Net.CookieContainer();

            var request = new RestRequest("authenticate", Method.POST);
            request.AddHeader("postman-token", "c7ea7240-427e-df9e-01e5-648428eb669c");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\n    \"email\": \"" + Config.email + "\",\n\"password\": \"" + Config.password + "\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            ViewBag.response = response.Content;
            
            request = new RestRequest("accounts", Method.GET);
            request.AddHeader("postman-token", "57452f76-6ac1-bd26-4db6-5ac23611e722");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\n    \"email\": \"j.moore@kainos.com\",\n\"password\": \"Hungryhungryhippos9!\"\n}", ParameterType.RequestBody);
            IRestResponse response3 = client.Execute(request);

            request = new RestRequest("my_vm", Method.GET);
            request.AddHeader("postman-token", "124fc3f6-f149-1dac-9726-6b5c271db8ee");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\n    \"email\": \"" + Config.email + "\",\n\"password\": \"" + Config.password + "\"\n}", ParameterType.RequestBody);
            IRestResponse response2 = client.Execute(request);
            //ViewBag.response2 = response2.Content;

            Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content);

            VM nicePrettyObject = JsonConvert.DeserializeObject<VM>(response2.Content);
            ViewBag.response2 = nicePrettyObject;
            List<VM> vms = new List<VM>();
            ViewBag.nicePrettyObject = values;

            Account fish = JsonConvert.DeserializeObject<Account>(response3.Content);
            ViewBag.response3 = fish.name;




            //var thingsCostingMoreThan100 = vms.Where(x => x.Price > 100);

            //var totalCostPerMonth = vms.Sum(x => x.Price);

            // LINQ



            return View();
        }
    }
}