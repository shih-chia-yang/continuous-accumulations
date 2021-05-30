using System;
using System.Linq;
using System.Threading.Tasks;
using marketplace.domain;
using marketplace.domain.entities;
using marketplace.domain.kernel;
using marketplace.domain.repositories;

namespace marketplace.infrastructure.repositories
{
    public class ClassifiedAdRepository : IClassifiedAdRepository
    {
        private readonly ClassifiedAdContext _context;

        public IUnitOfWork UnitOfWork => _context;
        public ClassifiedAdRepository(ClassifiedAdContext context)
        {
            _context = context;
        }
        public async Task<bool> Exists (Guid id)
        {
            return await Load(id) != null;
        }

        public async Task<ClassifiedAd> Load (Guid id)
        {
            return await _context.ClassifiedAds.FindAsync(id);
        }

        public async Task Add(ClassifiedAd entity)
        {
            await _context.ClassifiedAds.AddAsync(entity);
        }
    }
}