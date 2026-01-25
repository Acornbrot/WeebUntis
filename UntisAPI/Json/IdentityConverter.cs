using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UntisAPI.ResourceTypes;

namespace UntisAPI.Json
{
    public class IdentityConverter : JsonConverter<Identity>
    {
        public override Identity ReadJson(
            JsonReader reader,
            Type objectType,
            Identity? existingValue,
            bool hasExistingValue,
            JsonSerializer serializer
        )
        {
            JObject obj = JObject.Load(reader);

            Student student =
                obj["preSelected"]?.ToObject<Student>()
                ?? throw new JsonException(
                    "Required property 'preSelected' not present while trying to deserialize identity."
                );
            List<Class> classes = [];
            JArray? classesArray = obj["classes"] as JArray;

            if (classesArray is not null)
            {
                foreach (JToken item in classesArray)
                {
                    Class? @class = item["class"]?.ToObject<Class>();
                    if (@class is not null)
                    {
                        classes.Add(@class);
                    }
                }
            }

            return new Identity { Student = student, Classes = classes };
        }

        public override void WriteJson(
            JsonWriter writer,
            Identity? value,
            JsonSerializer serializer
        )
        {
            throw new NotImplementedException("For now, serializing Identity is not needed.");
        }
    }
}
