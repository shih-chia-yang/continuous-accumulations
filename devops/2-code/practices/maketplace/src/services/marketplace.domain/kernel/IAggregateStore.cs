using System.Threading.Tasks;

namespace marketplace.domain.kernel
{
    public interface IAggregateStore
    {
        Task<bool> Exists<T, TId>(TId aggregateId);
        Task Save<T, TId>(T aggregate) where T : AggregateRoot;
        Task<T> Load<T, TId>(TId aggregateId)where T:AggregateRoot;
    }
}