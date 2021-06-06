using System;
using System.Threading.Tasks;
using marketplace.domain.kernel;

namespace marketplace.api.Applications.Services
{
    public static class AppServiceExtensions
    {
        public static async Task HandleUpdate<T,TId>(
            this IAppService service,
            IAggregateStore store,
            TId aggregateId,
            Action<T> operation
        )where T:AggregateRoot
        {
            var aggregate = await store.Load<T,TId>(aggregateId);
            if(aggregate==null)
                throw new InvalidOperationException($"Entity with id {aggregateId.ToString()} cannot be found");
            operation(aggregate);
            await store.Save<T, TId>(aggregate);
        }
    }
}