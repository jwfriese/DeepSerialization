using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tests.Helpers;
using Tests.Models;
using Xunit;

namespace Tests
{
    public class DeserializeNestedObjectTests
    {
        [Fact]
        public void DeserializeNestedObject_WhenEverythingIsThere_CanDoIt()
        {
            var expectedRoom = new Room
            {
                CarpetColor = "green",
                Bed = new Bed
                {
                    NumberOfLegs = 4,
                    HasHeadboard = true,
                    CoverColor = "blue",
                },
            };

            var expectedHouse = new House
            {
                RoofColor = "someColor",
                NumberOfWindows = 5,
                Room = expectedRoom,
            };

            var json = new JObject
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

            var jsonString = json.ToString(); 

            var deserializedHouse = JsonConvert.DeserializeObject<House>(jsonString);
            Assert.Equal(expectedHouse, deserializedHouse);
        }

        [Fact]
        public void DeserializeNestedObject_WhenAnObjectIsMissing_SupportsDefaultValueFactoryAtLeafLevel()
        {
            var expectedRoom = new Room
            {
                CarpetColor = "green",
                Bed = new Bed()
                {
                    CoverColor = "",
                    HasHeadboard = false,
                    NumberOfLegs = 0,
                },
            };

            var expectedHouse = new House
            {
                RoofColor = "someColor",
                NumberOfWindows = 5,
                Room = expectedRoom,
            };

            var json = new JObject
            {
                {"NumberOfWindows", 5},
                {"RoofColor", "someColor"},
                {
                    "Room", new JObject
                    {
                        {"CarpetColor", "green"},
                    }
                },
            };

            var jsonString = json.ToString(); 

            var deserializedHouse = JsonConvert.DeserializeObject<House>(jsonString);
            Assert.Equal(expectedHouse, deserializedHouse);
        }
        
        [Fact]
        public void DeserializeNestedObject_WhenAnObjectIsMissing_SupportsDefaultValueFactoryAtAllDepthLevels()
        {
            var expectedRoom = new Room
            {
                CarpetColor = "",
                Bed = new Bed()
                {
                    CoverColor = "",
                    HasHeadboard = false,
                    NumberOfLegs = 0,
                },
            };

            var expectedHouse = new House
            {
                RoofColor = "someColor",
                NumberOfWindows = 5,
                Room = expectedRoom,
            };

            var json = new JObject
            {
                {"NumberOfWindows", 5},
                {"RoofColor", "someColor"},
            };

            var jsonString = json.ToString(); 

            var deserializedHouse = JsonConvert.DeserializeObject<House>(jsonString);
            Assert.Equal(expectedHouse, deserializedHouse);
        }
    }
}