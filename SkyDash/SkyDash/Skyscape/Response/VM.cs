using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyDash.Skyscape.Response
{
    public class VM
    {
       // [Json("vapps")]
        public List<VApps> VirtualApps { get; set; }
    }
}