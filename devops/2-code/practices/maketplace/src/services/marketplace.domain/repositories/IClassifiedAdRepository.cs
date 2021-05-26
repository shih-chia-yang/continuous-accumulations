using System.Threading.Tasks;

namespace marketplace.domain.repositories
{
    public interface IClassifiedAdRepository
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
        /// persists an entity
        /// </summary>
        /// <param name="entity">entity</param>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <returns></returns>
        Task Save (ClassifiedAd entity);

        /// <summary>
        /// check if entity with a given id already exist
        /// </summary>
        /// <param name="id">entity id</param>
        /// <typeparam name="TEntity">entity type</typeparam>
        /// <returns></returns>
        Task<bool> Exists (string id);
    }
}