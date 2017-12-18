using Newtonsoft.Json;
using PickyJson;

namespace Tests.Models
{
    public class NullReplacementRoomFactory: INullReplacementFactory
    {
        public object CreateReplacement()
        {
            return new Room();
        }
    }
    
    [JsonConverter(typeof(PickySerializer))]
    public class House
    {
        [UseNullReplacementFactory(typeof(NullStringReplacementFactory))]
        public string RoofColor { get; set; }
        public int NumberOfWindows { get; set; }
        
        [UseNullReplacementFactory(typeof(NullReplacementRoomFactory))]
        public Room Room { get; set; }
    }
}