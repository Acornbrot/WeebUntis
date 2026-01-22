using Newtonsoft.Json;

namespace UntisAPI.ResourceTypes
{
    public class Day
    {
        [JsonProperty("date")]
        public required DateTimeOffset Date;

        [JsonProperty("status")]
        public required UntisStatus Status;

        [JsonProperty("gridEntries")]
        public required List<Lesson> Lessons;
    }
}
