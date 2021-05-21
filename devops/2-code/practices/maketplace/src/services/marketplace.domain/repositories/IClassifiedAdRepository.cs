using System.Threading.Tasks;

namespace marketplace.domain.repositories
{
    public interface IClassifiedAdRepository
    {
        Task<TEntity> Load<TEntity>(string id);
        Task Save<TEntity>(TEntity entity);
    }
}