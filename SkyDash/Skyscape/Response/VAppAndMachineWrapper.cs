using System.Collections.Generic;
using Newtonsoft.Json;

namespace Skydash.Response
{
    public class VAppAndMachineWrapper
    {
        [JsonProperty("vapps")]
        public List<Dictionary<string, List<VirtualApp>>> VirtualApps { get; set; }

        [JsonProperty("vms")]
        public List<Dictionary<string, List<VirtualMachine>>> VirtualMachines { get; set; }
    }
}