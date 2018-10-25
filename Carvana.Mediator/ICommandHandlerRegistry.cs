using System;
using System.Threading.Tasks;

namespace Carvana.Mediator
{
    public interface ICommandHandlerRegistry 
    {
        void RegisterCommand<TCommand>(Func<TCommand, Task> onCommand);
    }
}
