using Newtonsoft.Json;

namespace UntisAPI.ResourceTypes
{
    public class Day
    {
        [JsonProperty("date")]
        public required DateTimeOffset Date { get; set; }

        [JsonProperty("status")]
        public required UntisStatus Status { get; set; }

        [JsonProperty("gridEntries")]
        public required List<Lesson> Lessons { get; set; }
    }
}
