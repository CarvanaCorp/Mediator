using System;
using System.Threading.Tasks;

namespace Carvana.Mediator
{
    public sealed class InMemoryCommands : ICommandHandler, ICommandHandlerRegistry
    {
        private readonly InMemoryEvents _commands;
        
        public InMemoryCommands()
        {
            _commands = new InMemoryEvents(InMemoryEvents.RegistrationRequirements.ExactlyOnePerType);
        }
        
        public async Task Execute<T>(T command)
        {
            await _commands.Publish(command);
        }

        public void RegisterCommand<TCommand>(Func<TCommand, Task> onCommand)
        {
            _commands.RegisterEvent(onCommand);
        }
    }
}
