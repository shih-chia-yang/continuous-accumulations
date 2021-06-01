using System;
using System.Linq;
using System.Threading.Tasks;
using marketplace.domain;
using marketplace.domain.entities;
using marketplace.domain.kernel;
using marketplace.domain.repositories;
using Microsoft.EntityFrameworkCore;

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


            // var classifiedAd = await _context.ClassifiedAds.FindAsync(id);

            var classifiedAd =await _context.ClassifiedAds
            .Include(x=>x.Pictures)
            .FirstOrDefaultAsync();

            // if(classifiedAd!=null)
            // {
            //     await _context.Entry(classifiedAd).Collection(i=>i.Pictures).LoadAsync();
            // }
            return classifiedAd;


            // return await _context.ClassifiedAds.FindAsync(id);
        }

        public void Add(ClassifiedAd entity)
        {
             _context.ClassifiedAds.Add(entity);
        }

        public void Update(ClassifiedAd entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}