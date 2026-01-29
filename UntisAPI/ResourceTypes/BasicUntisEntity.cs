using Newtonsoft.Json;

namespace UntisAPI.ResourceTypes
{
    public abstract class BasicUntisEntity
    {
        [JsonProperty("status")]
        public required UntisStatus Status { get; set; }

        [JsonProperty("shortName")]
        public required string ShortName { get; set; }

        [JsonProperty("longName")]
        public required string LongName { get; set; }

        [JsonProperty("displayName")]
        public required string DisplayName { get; set; }
    }
}
