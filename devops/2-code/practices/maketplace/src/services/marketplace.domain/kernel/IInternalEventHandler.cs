namespace marketplace.domain.kernel
{
    public interface IInternalEventHandler
    {
        void Handle(object @event);
    }
}