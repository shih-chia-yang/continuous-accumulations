using System.Threading.Tasks;

namespace marketplace.api.Applications
{
    public interface ICommandHandler<in TRequest>
    // where TRequest:class
    {
        Task Handle (TRequest command);
    }
}