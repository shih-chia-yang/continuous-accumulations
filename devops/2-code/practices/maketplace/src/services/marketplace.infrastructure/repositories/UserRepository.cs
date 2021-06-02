using System;
using System.Threading.Tasks;
using marketplace.domain.AggregateModels;
using marketplace.domain.AggregateModels.UserAggregate;
using marketplace.domain.kernel;
using marketplace.domain.repositories;

namespace marketplace.infrastructure.repositories
{
    public class UserRepository : IUserRepository
    {
        private ClassifiedAdContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public UserRepository(ClassifiedAdContext context)
        {
            _context = context;
        }
        public async Task AddAsync(UserProfile entity)
        {
            await _context.UserProfiles.AddAsync(entity);
        }

        public async Task<bool> ExistsAsync(UserId id)
        {
            return await LoadAsync(id) != null;
        }

        public async Task<UserProfile> LoadAsync(UserId id)
        {
            return await _context.UserProfiles.FindAsync(id);
        }
    }
}