using System.Threading.Tasks;
using marketplace.domain.AggregateModels;
using marketplace.domain.AggregateModels.UserAggregate;
using marketplace.domain.kernel;

namespace marketplace.domain.repositories
{
    public interface IUserRepository:IRepository
    {
        Task<UserProfile> Load(UserId id);

        Task Add(UserProfile entity);

        Task<bool> Exists(UserProfile id);

    }
}