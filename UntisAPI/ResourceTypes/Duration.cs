using Newtonsoft.Json;

namespace UntisAPI.ResourceTypes
{
    public class Duration
    {
        [JsonProperty("start")]
        public required DateTimeOffset Start;

        [JsonProperty("end")]
        public required DateTimeOffset End;
    }
}
