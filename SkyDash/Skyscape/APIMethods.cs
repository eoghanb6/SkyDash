using RestSharp;
using System.Net;

namespace SkyDash.Skyscape
{
    //This class contains all API calls made within application
    //Headers generated within Postman
    public class APIMethods
    {
        //two different clients for backups and snapshots
        RestClient skyscapeClient; //makes call to skyscape portal - backups
        RestClient vCloudClient; //makes call to vcloud director - snapshots

        public APIMethods()
        {
            //set up clients
            skyscapeClient = new RestClient("https://portal.skyscapecloud.com/api");
            vCloudClient = new RestClient("https://api.vcd.portal.skyscapecloud.com/api");
            skyscapeClient.CookieContainer = new CookieContainer();
        }

        //authentication for skyscape portal -- POST
        public IRestResponse authenticateSkyscape(string email, string password)

        {
            var request = new RestRequest("authenticate", Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\n    \"email\": \"" + email + "\",\n\"password\": \"" + password + "\"\n}", ParameterType.RequestBody);
            IRestResponse response = skyscapeClient.Execute(request);
            return response;
        }

        //get Accounts attached to login -- GET
        public IRestResponse getAccounts()
        {
            var request = new RestRequest("accounts", Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = skyscapeClient.Execute(request);
            return response;
        }

        //get Vms for skyscape portal AS json string -- GET
        public IRestResponse getSkyscapeVms(int accountID)
        {
            var request = new RestRequest("accounts/" + accountID + "/compute_services", Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = skyscapeClient.Execute(request);
            return response;
        }

        //Get Vcloud credentials to use in authentication -- GET
        public IRestResponse getVCloudCreds(int accountID)
        {
            var request = new RestRequest("accounts/" + accountID + "/api_credentials", Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = skyscapeClient.Execute(request);
            return response;
        }

        //Authenticate Vcloud account -- POST 
        public string authenticateVCloud(string encodedCredentials)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.vcd.portal.skyscapecloud.com/api/login");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "Accept=text/html,application/xhtml+xml,application/*+xml;version=1.5,**;q=0.8";
            request.Headers.Add("authorization", "Basic " + encodedCredentials);
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string vCloudToken = response.Headers["x-vcloud-authorization"].ToString();
                return vCloudToken;
            }
            catch
            {
                return null;
            }
        }

        // Get VMs from vCloud -- GET 
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
        // Get snapshots from Vcloud VMs - GET
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