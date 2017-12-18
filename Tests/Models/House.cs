using System;
using Newtonsoft.Json;
using PickyJson;

namespace Tests.Models
{
    public class NullReplacementRoomFactory: INullReplacementFactory
    {
        public object CreateReplacement()
        {
            var bedReplacementFactory = new NullReplacementBedFactory();
            return new Room()
            {
                CarpetColor = "",
                Bed = (Bed)bedReplacementFactory.CreateReplacement(),
            };

        }
    }
    
    [JsonConverter(typeof(PickySerializer))]
    public class House : IEquatable<House>
    {
        [UseNullReplacementFactory(typeof(NullStringReplacementFactory))]
        public string RoofColor { get; set; }
        public int NumberOfWindows { get; set; }
        
        [UseNullReplacementFactory(typeof(NullReplacementRoomFactory))]
        public Room Room { get; set; }

        public bool Equals(House other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(RoofColor, other.RoofColor) && NumberOfWindows == other.NumberOfWindows && Equals(Room, other.Room);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((House) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (RoofColor != null ? RoofColor.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ NumberOfWindows;
                hashCode = (hashCode * 397) ^ (Room != null ? Room.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}