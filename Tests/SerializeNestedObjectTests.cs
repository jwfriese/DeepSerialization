using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tests.Helpers;
using Tests.Models;
using Xunit;

namespace Tests
{
    public class SerializeNestedObjectTests
    {
        [Fact]
        public void SerializeNestedObject_WhenEverythingIsThere_CanDoIt()
        {
            var room = new Room
            {
                CarpetColor = "green",
                Bed = new Bed
                {
                    NumberOfLegs = 4,
                    HasHeadboard = true,
                    CoverColor = "blue",
                },
            };

            var house = new House
            {
                RoofColor = "someColor",
                NumberOfWindows = 5,
                Room = room,
            };

            var jsonString = JsonConvert.SerializeObject(house);
            var expectedJson = new JObject
            {
                {"NumberOfWindows", 5},
                {"RoofColor", "someColor"},
                {
                    "Room", new JObject
                    {
                        {"CarpetColor", "green"},
                        {
                            "Bed", new JObject
                            {
                                {"NumberOfLegs", 4},
                                {"HasHeadboard", true},
                                {"CoverColor", "blue"},
                            }
                        }
                    }
                },
            };

            var json = JToken.Parse(jsonString);
            Assert.True(JToken.DeepEquals(expectedJson, json));
        }

        [Fact]
        public void SerializeNestedObject_WhenAnObjectIsMissing_SupportsDefaultValueFactoryAtLeafLevel()
        {
            var room = new Room
            {
                CarpetColor = "green",
                Bed = null,
            };

            var house = new House
            {
                RoofColor = "someColor",
                NumberOfWindows = 5,
                Room = room,
            };

            var jsonString = JsonConvert.SerializeObject(house);
            var expectedJson = new JObject
            {
                {"NumberOfWindows", 5},
                {"RoofColor", "someColor"},
                {
                    "Room", new JObject
                    {
                        {"CarpetColor", "green"},
                        {
                            "Bed", new JObject
                            {
                                {"NumberOfLegs", 0},
                                {"HasHeadboard", false},
                                {"CoverColor", ""},
                            }
                        },
                    }
                },
            };

            var json = JToken.Parse(jsonString);
            Test.AssertSameJson(expectedJson, json);
        }
        
        [Fact]
        public void SerializeNestedObject_WhenAnObjectIsMissing_SupportsDefaultValueFactoryAtAllDepthLevels()
        {
            var house = new House
            {
                RoofColor = "someColor",
                NumberOfWindows = 5,
                Room = null,
            };

            var jsonString = JsonConvert.SerializeObject(house);
            var expectedJson = new JObject
            {
                {"NumberOfWindows", 5},
                {"RoofColor", "someColor"},
                {
                    "Room", new JObject
                    {
                        {"CarpetColor", ""},
                        {
                            "Bed", new JObject
                            {
                                {"NumberOfLegs", 0},
                                {"HasHeadboard", false},
                                {"CoverColor", ""},
                            }
                        },
                    }
                },
            };

            var json = JToken.Parse(jsonString);
            Test.AssertSameJson(expectedJson, json);
        }
    }
}