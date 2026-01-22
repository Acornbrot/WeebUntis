using Newtonsoft.Json;

namespace UntisAPI.ResourceTypes
{
    public class Class : BasicUntisEntity
    {
        [JsonProperty("id")]
        public required int Id;
    }
}
