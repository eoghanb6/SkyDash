using System;
using RestSharp;
using System.Linq;
using System.Net;
using System.IO;
using SkyDash.vCloud.Response;

namespace SkyDash.Skyscape
{
    public class APIMethods
    {
        RestClient skyscapeClient;
        RestClient vCloudClient;

        public APIMethods()
        {
            skyscapeClient = new RestClient("https://portal.skyscapecloud.com/api");
            vCloudClient = new RestClient("https://api.vcd.portal.skyscapecloud.com/api");
            skyscapeClient.CookieContainer = new System.Net.CookieContainer();
        }
        
        public IRestResponse authenticateSkyscape(String email, String password)
        {
            var request = new RestRequest("authenticate", Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\n    \"email\": \"" + email + "\",\n\"password\": \"" + password + "\"\n}", ParameterType.RequestBody);
            IRestResponse response = skyscapeClient.Execute(request);
            return response;
        }

        public IRestResponse getAccounts()
        {
            var request = new RestRequest("accounts", Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = skyscapeClient.Execute(request);
            return response;
        }

        public IRestResponse getSkyscapeVms(int accountID)
        {
            var request = new RestRequest("accounts/" + accountID + "/compute_services", Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = skyscapeClient.Execute(request);
            return response;
        }

        public IRestResponse getVCloudCreds(int accountID)
        {
            var request = new RestRequest("accounts/" + accountID + "/api_credentials", Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = skyscapeClient.Execute(request);
            return response;
        }

        public string authenticateVCloud(string encodedCredentials)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.vcd.portal.skyscapecloud.com/api/login");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "Accept=text/html,application/xhtml+xml,application/*+xml;version=1.5,**;q=0.8";
            request.Headers.Add("authorization", "Basic " + encodedCredentials);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string vCloudToken = response.Headers["x-vcloud-authorization"].ToString();
            return vCloudToken;
        }

        public HttpWebResponse getVCloudVms(string token)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.vcd.portal.skyscapecloud.com/api/vms/query");
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "Accept=text/html,application/xhtml+xml,application/*+xml;version=1.5,*/*;q=0.8";
            request.Headers.Add("x-vcloud-authorization", token);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response;
        }

        public HttpWebResponse getVCloudVmsSnapshots(string requestUrl, string token)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl + "/snapshotSection");
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "Accept=text/html,application/xhtml+xml,application/*+xml;version=5.6,*/*;q=0.8";
            request.Headers.Add("x-vcloud-authorization", token);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response;
        }
    }
}