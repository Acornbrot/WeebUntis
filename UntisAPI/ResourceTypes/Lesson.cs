using Newtonsoft.Json;

namespace UntisAPI.ResourceTypes
{
    public class Lesson
    {
        [JsonProperty("duration")]
        public required Duration Duration;

        [JsonProperty("type")]
        public required LessonType Type;

        [JsonProperty("status")]
        public required UntisStatus Status;

        [JsonProperty("layoutStartPosition")]
        public required int LayoutStartPosition;

        [JsonProperty("layoutWidth")]
        public required int LayoutWidth;

        [JsonProperty("layoutGroup")]
        public required int LayoutGroup;

        [JsonProperty("color")]
        public required string Color;

        [JsonProperty("notesAll")]
        public string? Notes;

        [JsonProperty("icons")]
        public List<Icon>? Icons;

        [JsonProperty("position1")]
        public List<PositionEntry<Subject>>? Subject;

        [JsonProperty("position2")]
        public List<PositionEntry<Teacher>>? Teacher;

        [JsonProperty("position3")]
        public List<PositionEntry<Room>>? Room;

        [JsonProperty("position4")]
        public List<PositionEntry<Info>>? Info;

        [JsonProperty("substitutionText")]
        public string? SubstitutionText;
    }
}
