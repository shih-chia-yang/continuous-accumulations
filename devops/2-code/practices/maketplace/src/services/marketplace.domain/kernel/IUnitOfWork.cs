using System.Threading.Tasks;

namespace marketplace.domain.kernel
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}