using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PickyJson;
using Tests.Helpers;
using Xunit;

namespace Tests
{
    public class NewtonsoftIntegrationTests
    {
        [JsonConverter(typeof(PickySerializer))]
        class ExampleObject
        {
            [JsonIgnore]
            public string ShouldNotSerialize { get; set; }
            
            public string ShouldIncludeWhenSerializing { get; set; }
        }

        [Fact]
        public void SerializationAndDeserialization_RespectsNewtonsoftAttributes()
        {
            var obj = new ExampleObject()
            {
                ShouldNotSerialize = "value should not get serialized",
                ShouldIncludeWhenSerializing = "value should get serialized",
            };

            var jsonString = JsonConvert.SerializeObject(obj);
            var expectedJson = new JObject
            {
                {"ShouldIncludeWhenSerializing", "value should get serialized"},
            };
            
            var json = JToken.Parse(jsonString);
            
            Test.AssertSameJson(expectedJson, json);
        }
    }
}