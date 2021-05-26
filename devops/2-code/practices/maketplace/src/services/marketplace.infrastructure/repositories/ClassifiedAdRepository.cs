using System.Threading.Tasks;
using marketplace.domain;
using marketplace.domain.repositories;

namespace marketplace.infrastructure.repositories
{
    public class ClassifiedAdRepository : IClassifiedAdRepository
    {
        public Task<bool> Exists (string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ClassifiedAd> Load (string id)
        {
            throw new System.NotImplementedException();
        }

        public Task Save (ClassifiedAd entity)
        {
            throw new System.NotImplementedException();
        }
    }
}