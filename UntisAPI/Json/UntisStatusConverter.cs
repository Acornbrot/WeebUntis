using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UntisAPI.ResourceTypes;

namespace UntisAPI.Json
{
    public class UntisStatusConverter : JsonConverter<UntisStatus>
    {
        public override UntisStatus ReadJson(
            JsonReader reader,
            Type objectType,
            UntisStatus existingValue,
            bool hasExistingValue,
            JsonSerializer serializer
        )
        {
            string value =
                reader.Value?.ToString()?.ToUpperInvariant()
                ?? throw new InvalidDataException("Required value for UntisStatus was null");

            return value switch
            {
                "REGULAR" => UntisStatus.Regular,
                "ADDED" => UntisStatus.Added,
                "REMOVED" => UntisStatus.Removed,
                "NOT_ALLOWED" => UntisStatus.NotAllowed,
                "NO_DATA" => UntisStatus.NoData,
                "CHANGED" => UntisStatus.Changed,
                "CANCELLED" => UntisStatus.Cancelled,
                _ => throw new InvalidDataException($"Unknown value {value} for enum UntisStatus"),
            };
        }

        public override void WriteJson(
            JsonWriter writer,
            UntisStatus value,
            JsonSerializer serializer
        )
        {
            throw new NotImplementedException("For now, serializing UntisStatus is not needed.");
        }
    }
}
