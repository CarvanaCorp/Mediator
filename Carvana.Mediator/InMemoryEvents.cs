using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Carvana.Mediator
{
    public sealed class InMemoryEvents : IEventHandler, IEventHandlerRegistry
    {
        private readonly RegistrationRequirements _registrationRequirements;
        private readonly Dictionary<Type, List<Func<object, Task>>> _handlers = new Dictionary<Type, List<Func<object, Task>>>();

        public enum RegistrationRequirements
        {
            None,
            ExactlyOnePerType,
        }

        public InMemoryEvents()
            : this(RegistrationRequirements.None) { }
        
        public InMemoryEvents(RegistrationRequirements registrationRequirements)
        {
            _registrationRequirements = registrationRequirements;
        }
        
        public async Task Publish<TEvent>(TEvent eventMessage)
        {
            var type = typeof(TEvent);
            var handlers = GetHandlers(type);
            if (_handlers.Count == 0 && _registrationRequirements == RegistrationRequirements.ExactlyOnePerType)
                throw new HandlerNotRegisteredException(type);
            
            foreach (var handler in handlers)
                await handler(eventMessage);
        }

        public void RegisterEvent<TEvent>(Func<TEvent, Task> onEvent)
        {
            var type = typeof(TEvent);
            if (!_handlers.ContainsKey(type))
                _handlers[type] = new List<Func<object, Task>>();
            
            if (_handlers[type].Count == 1 && _registrationRequirements == RegistrationRequirements.ExactlyOnePerType)
                throw new InvalidOperationException($"Only one handler for type {type.FullName} may be registered.");
            
            _handlers[type].Add(async x => await onEvent((TEvent)x));
        }

        private List<Func<object, Task>> GetHandlers(Type type)
        {
            return _handlers.ContainsKey(type)
                ? _handlers[type]
                : new List<Func<object, Task>>();
        }
    }
}
