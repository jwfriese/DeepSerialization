using System;

namespace DeepSerialization
{
    public class NullStringReplacementFactory : INullReplacementFactory
    {
        public object CreateReplacement()
        {
            return string.Empty;
        }
    }
}