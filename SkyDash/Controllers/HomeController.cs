using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSharp;

namespace SkyDash.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var client = new RestClient("https://portal.skyscapecloud.com/api/authenticate");
            client.CookieContainer = new System.Net.CookieContainer();
            var request = new RestRequest(Method.POST);
            request.AddHeader("postman-token", "c7ea7240-427e-df9e-01e5-648428eb669c");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\n\"email\": \"email\",\n\"password\": \"pass\"\n}\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            ViewBag.response = response.Content;

            client = new RestClient("https://portal.skyscapecloud.com/api/my_vm");
            
            request = new RestRequest(Method.GET);
            request.AddHeader("postman-token", "124fc3f6-f149-1dac-9726-6b5c271db8ee");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\n\"email\": \"email\",\n\"password\": \"pass\"\n}\n", ParameterType.RequestBody);
            IRestResponse response2 = client.Execute(request);
            ViewBag.response2 = response2.Content;

            return View();
        }
    }
}