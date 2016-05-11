using System;
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
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\n    \"email\": \"" + email + "\",\n\"password\": \"" + password + "\"\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return response;
        }

        public IRestResponse getAccounts()
        {
            var request = new RestRequest("accounts", Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = client.Execute(request);
            return response;
        }

        public IRestResponse getVms(int id)
        {
            var request = new RestRequest("accounts/" + id + "/compute_services", Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = client.Execute(request);
            return response;
        }
    }
}