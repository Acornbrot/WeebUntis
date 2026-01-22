using Newtonsoft.Json;

namespace UntisAPI.ResourceTypes
{
    public class Duration
    {
        [JsonProperty("start")]
        public DateTimeOffset Start;

        [JsonProperty("end")]
        public DateTimeOffset End;
    }
}
