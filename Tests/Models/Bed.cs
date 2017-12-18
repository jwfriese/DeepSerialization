using System;
using Newtonsoft.Json;
using PickyJson;

namespace Tests.Models
{
    [JsonConverter(typeof(PickySerializer))]
    public class Bed : IEquatable<Bed>
    {
        public int NumberOfLegs { get; set; }
        public bool HasHeadboard { get; set; }
        
        [UseNullReplacementFactory(typeof(NullStringReplacementFactory))]
        public string CoverColor { get; set; }

        public bool Equals(Bed other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return NumberOfLegs == other.NumberOfLegs && HasHeadboard == other.HasHeadboard && string.Equals(CoverColor, other.CoverColor);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Bed) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = NumberOfLegs;
                hashCode = (hashCode * 397) ^ HasHeadboard.GetHashCode();
                hashCode = (hashCode * 397) ^ (CoverColor != null ? CoverColor.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}