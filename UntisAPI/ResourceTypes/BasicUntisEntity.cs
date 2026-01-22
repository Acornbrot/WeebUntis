using Newtonsoft.Json;

namespace UntisAPI.ResourceTypes
{
    public abstract class BasicUntisEntity
    {
        [JsonProperty("status")]
        public UntisStatus Status;

        [JsonProperty("shortName")]
        public string ShortName;

        [JsonProperty("longName")]
        public string LongName;

        [JsonProperty("displayName")]
        public string DisplayName;
    }
}
