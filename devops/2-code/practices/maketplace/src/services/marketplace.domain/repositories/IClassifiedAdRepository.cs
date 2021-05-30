using System.Threading.Tasks;
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
        Task<ClassifiedAd> Load(string id);


        /// <summary>
        /// check if entity with a given id already exist
        /// </summary>
        /// <param name="id">entity id</param>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <returns></returns>
        Task<bool> Exists (string id);

        Task Add(ClassifiedAd entity);

    }
}