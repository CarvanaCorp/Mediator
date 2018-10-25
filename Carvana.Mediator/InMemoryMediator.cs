using System;
using System.Threading.Tasks;

namespace Carvana.Mediator
{
    public sealed class InMemoryMediator : IMediator
    {
        private readonly InMemoryCommands _commands;
        private readonly InMemoryEvents _events;
        private readonly InMemoryRequests _requests;

        public InMemoryMediator()
            : this(new InMemoryCommands(), new InMemoryEvents(), new InMemoryRequests()) { }
        
        public InMemoryMediator(InMemoryCommands commands, InMemoryEvents events, InMemoryRequests requests)
        {
            _commands = commands;
            _events = events;
            _requests = requests;
        }
        
        public async Task<TResponse> Handle<TRequest, TResponse>(TRequest request)
        {
            return await _requests.Handle<TRequest, TResponse>(request);
        }

        public async Task Execute<TCommand>(TCommand command)
        {
            await _commands.Execute(command);
        }
        
        public async Task Publish<TEvent>(TEvent eventMessage)
        {
            await _events.Publish(eventMessage);
        }

        public void Register<TRequest, TResponse>(Func<TRequest, Task<TResponse>> getResponse)
        {
            _requests.Register(getResponse);
        }

        public void RegisterCommand<TCommand>(Func<TCommand, Task> onCommand)
        {
            _commands.RegisterCommand(onCommand);
        }

        public void RegisterEvent<TEvent>(Func<TEvent, Task> onEvent)
        {
            _events.RegisterEvent(onEvent);
        }
    }
}
