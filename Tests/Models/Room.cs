using System;
using Newtonsoft.Json;
using PickyJson;

namespace Tests.Models
{
    public class NullReplacementBedFactory: INullReplacementFactory
    {
        public object CreateReplacement()
        {
            return new Bed()
            {
                CoverColor = "",
                HasHeadboard = false,
                NumberOfLegs = 0,
            };
        }
    }

    [JsonConverter(typeof(PickySerializer))]
    public class Room : IEquatable<Room>
    {
        [UseNullReplacementFactory(typeof(NullStringReplacementFactory))]
        public string CarpetColor { get; set; }
        
        [UseNullReplacementFactory(typeof(NullReplacementBedFactory))]
        public Bed Bed { get; set; }

        public bool Equals(Room other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(CarpetColor, other.CarpetColor) && Equals(Bed, other.Bed);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Room) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((CarpetColor != null ? CarpetColor.GetHashCode() : 0) * 397) ^ (Bed != null ? Bed.GetHashCode() : 0);
            }
        }
    }
}