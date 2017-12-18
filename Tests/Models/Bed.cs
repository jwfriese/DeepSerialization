using Newtonsoft.Json;
using PickyJson;

namespace Tests.Models
{
    [JsonConverter(typeof(PickySerializer))]
    public class Bed
    {
        public int NumberOfLegs { get; set; }
        public bool HasHeadboard { get; set; }
        
        [UseNullReplacementFactory(typeof(NullStringReplacementFactory))]
        public string CoverColor { get; set; }
    }
}