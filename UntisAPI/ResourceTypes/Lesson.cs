using Newtonsoft.Json;

namespace UntisAPI.ResourceTypes
{
    public class Lesson
    {
        [JsonProperty("duration")]
        public required Duration Duration { get; set; }

        [JsonProperty("type")]
        public required LessonType Type { get; set; }

        [JsonProperty("status")]
        public required UntisStatus Status { get; set; }

        [JsonProperty("layoutStartPosition")]
        public required int LayoutStartPosition { get; set; }

        [JsonProperty("layoutWidth")]
        public required int LayoutWidth { get; set; }

        [JsonProperty("layoutGroup")]
        public required int LayoutGroup { get; set; }

        [JsonProperty("color")]
        public required string Color { get; set; }

        [JsonProperty("notesAll")]
        public string? Notes { get; set; }

        [JsonProperty("icons")]
        public List<Icon>? Icons { get; set; }

        [JsonProperty("position1")]
        public List<PositionEntry<Subject>>? Subjects;

        [JsonIgnore]
        public PositionEntry<Subject>? Subject => Subjects?[0];

        [JsonProperty("position2")]
        public List<PositionEntry<Teacher>>? Teachers { get; set; }

        [JsonIgnore]
        public PositionEntry<Teacher>? Teacher => Teachers?[0];

        [JsonProperty("position3")]
        public List<PositionEntry<Room>>? Rooms { get; set; }

        [JsonIgnore]
        public PositionEntry<Room>? Room => Rooms?[0];

        [JsonProperty("position4")]
        public List<PositionEntry<Info>>? Infos { get; set; }

        [JsonIgnore]
        public PositionEntry<Info>? Info => Infos?[0];

        [JsonProperty("substitutionText")]
        public string? SubstitutionText { get; set; }
    }
}
