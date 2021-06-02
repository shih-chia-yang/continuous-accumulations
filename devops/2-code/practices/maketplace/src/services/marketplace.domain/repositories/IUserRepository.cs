using System;
using System.Threading.Tasks;
using marketplace.domain.AggregateModels;
using marketplace.domain.AggregateModels.UserAggregate;
using marketplace.domain.kernel;

namespace marketplace.domain.repositories
{
    public interface IUserRepository:IRepository
    {
        Task<UserProfile> LoadAsync(UserId id);

        Task AddAsync(UserProfile entity);

        Task<bool> ExistsAsync(UserId id);

    }
}