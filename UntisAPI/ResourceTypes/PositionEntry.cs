using Newtonsoft.Json;

namespace UntisAPI.ResourceTypes
{
    public class PositionEntry<T>
    {
        [JsonProperty("current")]
        public required T Current;

        [JsonProperty("removed")]
        public T? Removed;
    }
}
