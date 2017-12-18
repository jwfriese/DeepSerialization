using System;

namespace DeepSerialization
{
    [AttributeUsage(AttributeTargets.Property)]
    public class UseNullReplacementFactoryAttribute : Attribute
    {
        private readonly Type _type;

        public UseNullReplacementFactoryAttribute(Type type)
        {
            _type = type;
        }

        public INullReplacementFactory GetFactory()
        {
            // throw exception if _type is not an implementor
            return (INullReplacementFactory)Activator.CreateInstance(_type);
        }
    }
}