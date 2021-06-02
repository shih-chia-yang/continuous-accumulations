using System.Threading.Tasks;

namespace marketplace.domain.kernel.commands
{
    public interface ICommandHandler<in TRequest>
    where TRequest:class
    {
        Task Handle (TRequest command);
    }
}