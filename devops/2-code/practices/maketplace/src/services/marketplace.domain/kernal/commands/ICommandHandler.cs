using System.Threading.Tasks;

namespace marketplace.domain.kernal.commands
{
    public interface ICommandHandler<in TRequest>
    where TRequest:class
    {
        Task Handle (TRequest command);
    }
}