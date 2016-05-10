using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Globalization;

namespace Skydash.Response
{
    public class VirtualMachine
    {
        [JsonProperty("_id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("urn")]
        public string Urn { get; set; }
        
        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("monthToDate")]
        public string MonthToDate { get; set; }
        public decimal? CostToDate
        {
            get
            {
                if (MonthToDate != null)
                {
                    return decimal.Parse(MonthToDate, NumberStyles.Currency);
                }
                return null;
            }
        }

        [JsonProperty("estimatedMonthlyTotal")]
        public string EstimatedMonthlyTotal { get; set; }
        public decimal? EstimatedMonthlyCost
        {
            get
            {
                if (EstimatedMonthlyTotal != null)
                {
                    return decimal.Parse(EstimatedMonthlyTotal, NumberStyles.Currency);
                }
                return null;
            }
        }

        [JsonProperty("billedHoursPoweredOn")]
        public int? BilledHoursPoweredOn { get; set; }

        [JsonProperty("billedHoursPoweredOff")]
        public int? BilledHoursPoweredOff { get; set; }

        [JsonProperty("powerStatus")]
        public string PowerStatus { get; set; }

        [JsonProperty("operatingSystem")]
        public string OperatingSystem { get; set; }

        [JsonProperty("numberOfCPUs")]
        public string NumberOfCPUs { get; set; }

        [JsonProperty("memory")]
        public int Memory { get; set; }

        [JsonProperty("storage")]
        public int Storage { get; set; }

        [JsonProperty("lastBackupStatus")]
        public string LastBackupStatus { get; set; }

        [JsonProperty("inBackup")]
        public bool InBackup { get; set; }

        [JsonProperty("lastBackup")]
        public string LastBackup { get; set; }

        [JsonProperty("retentionLength")]
        public int? RetentionLength { get; set; }

        [JsonProperty("backups")]
        public List<Backup> Backups { get; set; }
    }

    public class PanelVM
    {
        public string Name;
        public string LastBackupStatus;
        public string LastBackup;
        public int AccountId;
    }
}