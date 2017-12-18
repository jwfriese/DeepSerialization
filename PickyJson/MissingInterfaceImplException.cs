using System;

namespace PickyJson
{
    public class MissingInterfaceImplException : Exception
    {
        public MissingInterfaceImplException(Type offendingType, Type expectedInterface)
        {
            Message =
                $"Expected type with name {offendingType.Name} to implement interface {expectedInterface.Name}";
        }

        public override string Message { get; }
    }
}