using Newtonsoft.Json;

namespace UntisAPI.ResourceTypes
{
    public class PositionEntry<T>
    {
        [JsonProperty("current")]
        public T? Current;

        [JsonProperty("removed")]
        public T? Removed;
    }
}
