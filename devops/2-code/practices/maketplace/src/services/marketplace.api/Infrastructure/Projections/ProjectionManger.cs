using System.Linq;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using marketplace.api.Infrastructure.Projections;
using Serilog;
using Serilog.Events;

namespace marketplace.api.Infrastructure
{
    
    public class ProjectionManger:ISubscription
    {
        public readonly IEventStoreConnection _connection;
        public readonly IProjection[] _projections;
        private EventStoreAllCatchUpSubscription _subscription;

        public ProjectionManger(IEventStoreConnection connection,params IProjection[] projections)
        {
            _connection = connection;
            _projections = projections;
        }

        public void Start()
        {
            var setting = new CatchUpSubscriptionSettings(
                2000,
                500,
                Log.IsEnabled(LogEventLevel.Verbose),
                false,
                "try-out-subscription"
                );
            _subscription = _connection.SubscribeToAllFrom(Position.Start, setting, EventAppeared);
        }

        public void Stop() => _subscription.Stop();

        private Task EventAppeared(EventStoreCatchUpSubscription _,ResolvedEvent resolvedEvent)
        {
            if(resolvedEvent.Event.EventType.StartsWith("$"))return Task.CompletedTask;
            var @event = resolvedEvent.Deserialize();
            Log.Debug($"Projecting event {@event.GetType().Name}");
            return Task.WhenAll(_projections.Select(x => x.Project(@event)));
        }
    }
}