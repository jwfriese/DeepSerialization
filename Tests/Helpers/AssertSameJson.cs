using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Tests.Helpers
{
    public class Test
    {
        public static void AssertSameJson(JToken expectedJson, JToken jsonUnderTest)
        {
            var isEqual = JToken.DeepEquals(expectedJson, jsonUnderTest);
            if (isEqual)
            {
                return;
            }
            
            var expectedJsonString = JsonConvert.SerializeObject(expectedJson, Formatting.Indented);
            var jsonUnderTestString = JsonConvert.SerializeObject(jsonUnderTest, Formatting.Indented);

            Assert.True(false, $"JSON under test is different from expected JSON.\nExpected:\n{expectedJsonString},\ngot:\n{jsonUnderTestString}\n");
        }
    }
}