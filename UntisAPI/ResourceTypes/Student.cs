using Newtonsoft.Json;

namespace UntisAPI.ResourceTypes
{
    public class Student : BasicUntisEntity
    {
        [JsonProperty("id")]
        public int Id;
    }
}
