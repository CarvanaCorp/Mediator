namespace Carvana.Mediator
{
    public interface IMediator : 
        IRequestHandler, IRequestHandlerRegistry, 
        ICommandHandler, ICommandHandlerRegistry, 
        IEventHandler, IEventHandlerRegistry
    {
    }
}
