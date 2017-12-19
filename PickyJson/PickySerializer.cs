using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
                var shouldSkip = property.GetCustomAttributes()
                    .OfType<JsonIgnoreAttribute>()
                    .Any();
                if (shouldSkip)
                {
                    continue;
                }
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

        public override object ReadJson(JsonReader reader, Type objectType,
            object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }
            var jObject = JObject.Load(reader);
            var target = Activator.CreateInstance(objectType);
            using (var jObjectReader = CopyReaderForObject(reader, jObject))
            {
                serializer.Populate(jObjectReader, target);
            }

            var properties = target.GetType().GetProperties();
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(target, null);
                if (propertyValue == null)
                {
                    var defaultFactoryAttribute = property.GetCustomAttributes()
                        .OfType<UseNullReplacementFactoryAttribute>()
                        .FirstOrDefault();
                    if (defaultFactoryAttribute != null)
                    {
                        var factory = defaultFactoryAttribute.GetFactory();
                        property.SetValue(target, factory.CreateReplacement());
                    }
                }
            }

            return target;
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        private JsonReader CopyReaderForObject(JsonReader reader, JObject jObject)
        {
            JsonReader jObjectReader = jObject.CreateReader();
            jObjectReader.Culture = reader.Culture;
            jObjectReader.DateFormatString = reader.DateFormatString;
            jObjectReader.DateParseHandling = reader.DateParseHandling;
            jObjectReader.DateTimeZoneHandling = reader.DateTimeZoneHandling;
            jObjectReader.FloatParseHandling = reader.FloatParseHandling;
            jObjectReader.MaxDepth = reader.MaxDepth;
            jObjectReader.SupportMultipleContent = reader.SupportMultipleContent;
            return jObjectReader;
        }
    }
}