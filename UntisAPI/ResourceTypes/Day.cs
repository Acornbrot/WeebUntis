using Newtonsoft.Json;

namespace UntisAPI.ResourceTypes
{
    public class Day
    {
        [JsonProperty("date")]
        public DateTimeOffset Date;

        [JsonProperty("status")]
        public UntisStatus Status;

        [JsonProperty("gridEntries")]
        public List<Lesson> Lessons;
    }
}
