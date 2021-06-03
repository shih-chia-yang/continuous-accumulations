using System;
using System.Threading.Tasks;
using marketplace.domain.AggregateModels;
using marketplace.domain.AggregateModels.UserAggregate;
using marketplace.domain.kernel;

namespace marketplace.domain.repositories
{
    public interface IUserRepository:IRepository
    {
        Task<UserProfile> LoadAsync(Guid id);

        Task AddAsync(UserProfile entity);

        Task<bool> ExistsAsync(Guid id);

    }
}