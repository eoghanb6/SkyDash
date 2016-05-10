using Newtonsoft.Json;
using System.Collections.Generic;

namespace Skydash.Response
{
    public class VirtualApp
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("urn")]
        public string Urn { get; set; }

        [JsonProperty("VMs")]
        public List<VirtualMachine> VMs { get; set; }
    }
}