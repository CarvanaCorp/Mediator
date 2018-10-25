using System;
using System.Threading.Tasks;

namespace Carvana.Mediator
{
    public interface IRequestHandlerRegistry
    {
        void Register<TRequest, TResponse>(Func<TRequest, Task<TResponse>> getResponse);
    }
}
