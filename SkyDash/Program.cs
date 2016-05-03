using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Skydash.Response;

namespace Skydash
{
    internal class Program
    {
        

        private static void Main(string[] args)
        {
            string json;
            
            using (StreamReader r = new StreamReader())
            {
                json = r.ReadToEnd();
            }

            //var result = JsonConvert.DeserializeObject<Dictionary<string, VAppAndMachineWrapper>>(json);
        }
    }
}