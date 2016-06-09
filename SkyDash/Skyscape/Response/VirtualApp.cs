using Newtonsoft.Json;
using System.Collections.Generic;

namespace Skydash.Response
{
    public class VirtualApp
    {
        //Gets and Sets VirtualApp properties
        //JsonProperty used to link with properties in JSON string
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("urn")]
        public string Urn { get; set; }

        [JsonProperty("VMs")]
        public List<VirtualMachine> VMs { get; set; }
    }
}