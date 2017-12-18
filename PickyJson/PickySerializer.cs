using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace PickyJson
{
    public class PickySerializer : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var properties = value.GetType().GetProperties();

            writer.WriteStartObject();

            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(value, null);
                if (propertyValue == null)
                {
                    var defaultFactoryAttribute = property.GetCustomAttributes()
                        .OfType<UseNullReplacementFactoryAttribute>()
                        .FirstOrDefault();
                    if (defaultFactoryAttribute != null)
                    {
                        writer.WritePropertyName(property.Name);
                        var factory = defaultFactoryAttribute.GetFactory();
                        serializer.Serialize(writer, factory.CreateReplacement());
                    }
                }
                else
                {
                    writer.WritePropertyName(property.Name);
                    serializer.Serialize(writer, property.GetValue(value, null));
                }
            }

            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
}