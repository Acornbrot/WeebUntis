using Newtonsoft.Json;

namespace UntisAPI.ResourceTypes
{
    public class PositionEntry<T>
    {
        [JsonProperty("current")]
        public T? Current { get; set; }

        [JsonProperty("removed")]
        public T? Removed { get; set; }
    }
}
