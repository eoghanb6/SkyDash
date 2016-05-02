using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSharp;

namespace SkyDash.Skyscape
{
    public class APIMethods
    {
        RestClient client;

        public APIMethods()
        {
            client = new RestClient("https://portal.skyscapecloud.com/api");
            client.CookieContainer = new System.Net.CookieContainer();
        }
        
        public IRestResponse authenticate(String email, String password)
        {
            var request = new RestRequest("authenticate", Method.POST);
            //request.AddHeader("postman-token", "c7ea7240-427e-df9e-01e5-648428eb669c");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\n    \"email\": \"" + email + "\",\n\"password\": \"" + password + "\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return response;
        }

        public IRestResponse getAccounts()
        {
            var request = new RestRequest("accounts", Method.GET);
            //request.AddHeader("postman-token", "57452f76-6ac1-bd26-4db6-5ac23611e722");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            //request.AddParameter("application/json", "{\n    \"email\": \"" + email + "\",\n\"password\": \"" + password + "\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return response;
        }

        public IRestResponse getVms()
        {
            var request = new RestRequest("my_vm", Method.GET);
            //request.AddHeader("postman-token", "124fc3f6-f149-1dac-9726-6b5c271db8ee");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            //request.AddParameter("application/json", "{\n    \"email\": \"" + email + "\",\n\"password\": \"" + password + "\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return response;
        }
    }
}