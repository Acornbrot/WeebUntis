using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UntisAPI.ResourceTypes;

namespace UntisAPI.Json
{
    public class LessonTypeConverter : JsonConverter<LessonType>
    {
        public override LessonType ReadJson(
            JsonReader reader,
            Type objectType,
            LessonType existingValue,
            bool hasExistingValue,
            JsonSerializer serializer
        )
        {
            string value =
                reader.Value?.ToString()?.ToUpperInvariant()
                ?? throw new InvalidDataException("Required value for UntisStatus was null");

            return value switch
            {
                "NORMAL_TEACHING_PERIOD" => LessonType.NormalTeaching,
                "EVENT" => LessonType.Event,
                _ => throw new InvalidDataException($"Unknown value {value} for enum LessonType"),
            };
        }

        public override void WriteJson(
            JsonWriter writer,
            LessonType value,
            JsonSerializer serializer
        )
        {
            throw new NotImplementedException("For now, serializing UntisStatus is not needed.");
        }
    }
}
