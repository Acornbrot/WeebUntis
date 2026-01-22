using Newtonsoft.Json;

namespace UntisAPI.ResourceTypes
{
    public abstract class BasicUntisEntity
    {
        [JsonProperty("status")]
        public required UntisStatus Status;

        [JsonProperty("shortName")]
        public required string ShortName;

        [JsonProperty("longName")]
        public required string LongName;

        [JsonProperty("displayName")]
        public required string DisplayName;
    }
}
