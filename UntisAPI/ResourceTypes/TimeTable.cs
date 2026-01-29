using Newtonsoft.Json;

namespace UntisAPI.ResourceTypes
{
    public class TimeTable
    {
        [JsonProperty("days")]
        public required List<Day> Days { get; set; }
    }
}
