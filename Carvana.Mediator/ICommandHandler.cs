using System.Threading.Tasks;

namespace Carvana.Mediator
{
    public interface ICommandHandler
    {
        Task Execute<T>(T command);
    }
}
