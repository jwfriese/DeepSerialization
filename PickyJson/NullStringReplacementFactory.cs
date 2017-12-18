using System;

namespace PickyJson
{
    public class NullStringReplacementFactory : INullReplacementFactory
    {
        public object CreateReplacement()
        {
            return string.Empty;
        }
    }
}