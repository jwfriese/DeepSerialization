using Newtonsoft.Json;
using PickyJson;

namespace Tests.Models
{
    public class NullReplacementBedFactory: INullReplacementFactory
    {
        public object CreateReplacement()
        {
            return new Bed();
        }
    }

    [JsonConverter(typeof(PickySerializer))]
    public class Room
    {
        [UseNullReplacementFactory(typeof(NullStringReplacementFactory))]
        public string CarpetColor { get; set; }
        
        [UseNullReplacementFactory(typeof(NullReplacementBedFactory))]
        public Bed Bed { get; set; }
    }
}