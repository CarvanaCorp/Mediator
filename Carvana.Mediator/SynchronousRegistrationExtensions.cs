using System;
using System.Threading.Tasks;

namespace Carvana.Mediator
{
    public static class SynchronousRegistrationExtensions
    {
        public static void HandleEvent<TMessage>(this IEventHandlerRegistry registry, Action<TMessage> onEvent)
        {
            registry.RegisterEvent<TMessage>(x =>
            {
                onEvent(x);
                return Task.CompletedTask;
            });
        }
        
        public static void HandleCommand<TMessage>(this ICommandHandlerRegistry registry, Action<TMessage> onEvent)
        {
            registry.RegisterCommand<TMessage>(x =>
            {
                onEvent(x);
                return Task.CompletedTask;
            });
        }
        
        public static void Handle<TRequest, TResponse>(this IRequestHandlerRegistry registry, Func<TRequest, TResponse> getResponse)
        {
            registry.Register<TRequest, TResponse>(x => Task.FromResult(getResponse(x)));
        }
    }
}
