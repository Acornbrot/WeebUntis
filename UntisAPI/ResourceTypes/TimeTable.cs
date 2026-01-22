using Newtonsoft.Json;

namespace UntisAPI.ResourceTypes
{
    public class TimeTable
    {
        [JsonProperty("days")]
        public List<Day> Days;
    }
}
