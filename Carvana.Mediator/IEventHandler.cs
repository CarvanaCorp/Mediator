using System.Threading.Tasks;

namespace Carvana.Mediator
{
    public interface IEventHandler
    {
        Task Publish<TEvent>(TEvent eventMessage);
    }
}
