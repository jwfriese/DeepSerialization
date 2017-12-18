using System;

namespace PickyJson
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
            var doesTypeImplementInterface = typeof(INullReplacementFactory).IsAssignableFrom(_type);
            if (doesTypeImplementInterface)
            {
                return (INullReplacementFactory) Activator.CreateInstance(_type);
            }

            throw new MissingInterfaceImplException(_type, typeof(INullReplacementFactory));
        }
    }
}