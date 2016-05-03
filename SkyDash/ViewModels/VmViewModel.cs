﻿using Skydash.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyDash.ViewModels
{
    public class VmViewModel
    {
        public int id { get; set; }
        public List<int> ids{ get; set; }
        public String name { get; set; }
        public String last_backup_status { get; set; }
        public List<Backup> backups { get; set; }

    }
}