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
        public List<PositionEntry<Subject>>? Subjects;

        [JsonIgnore]
        public PositionEntry<Subject>? Subject => Subjects?[0];

        [JsonProperty("position2")]
        public List<PositionEntry<Teacher>>? Teachers;

        [JsonIgnore]
        public PositionEntry<Teacher>? Teacher => Teachers?[0];

        [JsonProperty("position3")]
        public List<PositionEntry<Room>>? Rooms;

        [JsonIgnore]
        public PositionEntry<Room>? Room => Rooms?[0];

        [JsonProperty("position4")]
        public List<PositionEntry<Info>>? Info;

        [JsonProperty("substitutionText")]
        public string? SubstitutionText;
    }
}
