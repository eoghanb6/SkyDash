﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace Skydash.Response
{
    public class VirtualMachine
    {
        [JsonProperty("_id")]
        public int Id { get; set; }

        [JsonProperty("urn")]
        public string Urn { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("os")]
        public string OperatingSystem { get; set; }

        [JsonProperty("cpu")]
        public string CPU { get; set; }

        [JsonProperty("mem")]
        public string Memory { get; set; }

        [JsonProperty("storage")]
        public string Storage { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("month_to_date")]
        public string MonthToDate { get; set; }

        [JsonProperty("estimated_monthly_total")]
        public string EstimatedMonthlyTotal { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("last_backup_status")]
        public string LastBackupStatus { get; set; }

        [JsonProperty("in_backup")]
        public bool InBackup { get; set; }

        [JsonProperty("last_backup")]
        public string LastBackup { get; set; }

        [JsonProperty("retention_length")]
        public int RetentionLength { get; set; }

        [JsonProperty("billed_hours_powered_on")]
        public int BilledHoursPoweredOn { get; set; }

        [JsonProperty("billed_hours_powered_off")]
        public int BilledHoursPoweredOff { get; set; }

        [JsonProperty("backups")]
        public List<Backup> Backups { get; set; }
    }
}