using System;

namespace Carvana.Mediator
{
    public sealed class HandlerNotRegisteredException : Exception
    {
        public HandlerNotRegisteredException(Type type)
            : base($"Missing required registered handler for type {type.FullName}") { }
    }
}