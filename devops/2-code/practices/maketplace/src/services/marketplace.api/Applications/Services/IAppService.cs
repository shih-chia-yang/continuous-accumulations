using System.Threading.Tasks;

namespace marketplace.api.Applications
{
    public interface IAppService
    {
        Task Handle(object command);
    }
}