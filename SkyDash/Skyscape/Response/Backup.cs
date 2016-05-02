using Newtonsoft.Json;

namespace Skydash.Response
{
    public class Backup
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("backup_slot")]
        public string BackupSlot { get; set; }

        [JsonProperty("backup_start")]
        public string BackupStart { get; set; }

        [JsonProperty("backup_end")]
        public string BackupEnd { get; set; }

        [JsonProperty("snapshot_removal_start")]
        public string SnapshotRemovalStart { get; set; }

        [JsonProperty("snapshot_removal_end")]
        public string SnapshotRemovalEnd { get; set; }
    }
}