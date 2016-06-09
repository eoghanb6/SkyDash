using System.Collections.Generic;
using Newtonsoft.Json;
using System.Globalization;
using SkyDash.Skyscape.Response;

namespace Skydash.Response
{
    public class VirtualMachine
    {
        //Get and Set VM properties to set instances ov VM objects, matches with case sensitive JSON string
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
        public decimal? CostToDate //If decimal, must be changed to currency, only if > 0. No Cost property changed to Null rather than 0
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
        public decimal? EstimatedMonthlyCost //If decimal, must be changed to currency, only if > 0. No Cost property changed to Null rather than 0
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
        //Gets and Sets VM properties
        //JsonProperty used to link with properties in JSON string
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
    //Properties added to PanelVM view model
    public class PanelVM
    {
        public int Id;
        public string Name;
        public string LastBackupStatus;
        public string LastBackup;
        public int AccountId;
        public List<Backup> backups;
        public List<Account> Accounts;
        public int counter;
        public string Size;
        public string MonthToDate;
        public string EstimatedMonthlyTotal;
        public int? BilledHoursPoweredOn;
        public int? BilledHoursPoweredOff;
        public string PowerStatus;
        public string OperatingSystem;
        public string NumberOfCPUs;
        public int Memory;
        public int Storage;
        public string lastbackupStatus;
    }
}