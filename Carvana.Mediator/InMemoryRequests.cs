using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Carvana.Mediator
{
    public sealed class InMemoryRequests : IRequestHandler, IRequestHandlerRegistry
    {
        private readonly Dictionary<Type, Func<object, Task<object>>> _handlers = new Dictionary<Type, Func<object, Task<object>>>();
        
        public async Task<TResponse> Handle<TRequest, TResponse>(TRequest request)
        {
            var type = typeof(TRequest);
            if (!_handlers.ContainsKey(type))
                throw new HandlerNotRegisteredException(type);
            return (TResponse)await _handlers[type](request);
        }

        public void Register<TRequest, TResponse>(Func<TRequest, Task<TResponse>> getResponse)
        {
            var type = typeof(TRequest);
            if (_handlers.ContainsKey(type))
                throw new InvalidOperationException($"Only one handler for type {type.FullName} may be registered.");
            _handlers[type] = (async x => await getResponse((TRequest)x));
        }
    }
}
