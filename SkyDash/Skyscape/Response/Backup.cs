using Newtonsoft.Json;
using System;

namespace Skydash.Response
{
    public class Backup
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("backup_slot")]
        public string BackupSlot { get; set; }

        [JsonProperty("backup_start")]
        public DateTime? BackupStart { get; set; }

        [JsonProperty("backup_end")]
        public DateTime? BackupEnd { get; set; }

        [JsonProperty("snapshot_removal_start")]
        public DateTime? SnapshotRemovalStart { get; set; }

        [JsonProperty("snapshot_removal_end")]
        public DateTime? SnapshotRemovalEnd { get; set; }
    }
}