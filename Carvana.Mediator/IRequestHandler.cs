using System.Threading.Tasks;

namespace Carvana.Mediator
{
    public interface IRequestHandler
    {
        Task<TResponse> Handle<TRequest, TResponse>(TRequest request);
    }
}
