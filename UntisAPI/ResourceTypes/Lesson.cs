using Newtonsoft.Json;

namespace UntisAPI.ResourceTypes
{
    public class Lesson
    {
        [JsonProperty("duration")]
        public Duration Duration;

        [JsonProperty("type")]
        public LessonType Type;

        [JsonProperty("status")]
        public UntisStatus Status;

        [JsonProperty("layoutStartPosition")]
        public int LayoutStartPosition;

        [JsonProperty("layoutWidth")]
        public int LayoutWidth;

        [JsonProperty("layoutGroup")]
        public int LayoutGroup;

        [JsonProperty("color")]
        public string Color;

        [JsonProperty("notesAll")]
        public string Notes;

        [JsonProperty("icons")]
        public List<Icon> Icons;

        [JsonProperty("position1")]
        public List<PositionEntry<Subject>> Subject;

        [JsonProperty("position2")]
        public List<PositionEntry<Teacher>> Teacher;

        [JsonProperty("position3")]
        public List<PositionEntry<Room>> Room;

        [JsonProperty("position4")]
        public List<PositionEntry<Info>> Info;

        [JsonProperty("substitutionText")]
        public string SubstitutionText;
    }
}
