using DeepSerialization;
using Newtonsoft.Json;

namespace Tests.Models
{
    [JsonConverter(typeof(DeepSerializer))]
    public class Bed
    {
        public int NumberOfLegs { get; set; }
        public bool HasHeadboard { get; set; }
        
        [UseNullReplacementFactory(typeof(NullStringReplacementFactory))]
        public string CoverColor { get; set; }
    }
}