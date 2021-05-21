using System.Threading.Tasks;
using marketplace.domain.repositories;

namespace marketplace.infrastructure.repositories
{
    public class ClassifiedAdRepository : IClassifiedAdRepository
    {
        public Task<TEntity> Load<TEntity>(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task Save<TEntity>(TEntity entity)
        {
            throw new System.NotImplementedException();
        }
    }
}