using System;
using System.Threading.Tasks;
using marketplace.domain.AggregateModels.ClassifiedAdAggregate;
using marketplace.domain.kernel;

namespace marketplace.domain.repositories
{
    public interface IClassifiedAdRepository:IRepository
    {
        /// <summary>
        /// loads an entity by id
        /// </summary>
        /// <param name="id">entity id</param>
        /// <typeparam name="TEntity">entity type
        /// </typeparam>
        /// <returns></returns>
        Task<ClassifiedAd> Load(Guid id);


        /// <summary>
        /// check if entity with a given id already exist
        /// </summary>
        /// <param name="id">entity id</param>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <returns></returns>
        Task<bool> Exists (Guid id);

        void Add(ClassifiedAd entity);

        void Update(ClassifiedAd entity);

    }
}