using System;
using System.Threading.Tasks;

namespace Carvana.Mediator
{
    public interface IEventHandlerRegistry
    {
        void RegisterEvent<TEvent>(Func<TEvent, Task> onEvent);
    }
}
