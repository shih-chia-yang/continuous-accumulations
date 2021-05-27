namespace marketplace.domain.kernal
{
    public interface IInternalEventHandler
    {
        void Handle(object @event);
    }
}