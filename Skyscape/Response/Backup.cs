using Newtonsoft.Json;
using System;

namespace Skydash.Response
{
    public class Backup
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("backupSlot")]
        public string BackupSlot { get; set; }

        [JsonProperty("backupStart")]
        public DateTime? BackupStart { get; set; }

        [JsonProperty("backupEnd")]
        public DateTime? BackupEnd { get; set; }

        [JsonProperty("snapshotRemovalStart")]
        public DateTime? SnapshotRemovalStart { get; set; }

        [JsonProperty("snapshotRemovalEnd")]
        public DateTime? SnapshotRemovalEnd { get; set; }
    }
}