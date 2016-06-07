using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LINQtoCSV;

namespace SkyDash.Skyscape.Response
{
    public class Snapshot
    {   
        [CsvColumn(FieldIndex = 1)]
        public string Name { get; set; }

        [CsvColumn(FieldIndex = 2)]
        public string Status { get; set; }

        [CsvColumn(FieldIndex = 3)]
        public string vApp { get; set; }

        [CsvColumn(FieldIndex = 4)]
        public string vDC { get; set; }

        [CsvColumn(FieldIndex = 5)]
        public int SnapshotSize { get; set; }

        [CsvColumn(FieldIndex = 6, OutputFormat = "MM/dd/YYYY HH:mm") ]
        public DateTime DateCreated { get; set; }

        public int accountId;
        public int counter;
    }
}