using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using marketplace.api.ViewModels;
using marketplace.domain.events.ClassifiedAdEvents;
using Serilog.Events;
using Serilog;
using ILogger = Serilog.ILogger;

namespace marketplace.api.Infrastructure
{
    public interface ISubscription
    {
        void Start();
        void Stop();
    }

    public class EsSubscription:ISubscription
    {
        private static ILogger Log =Serilog.Log.ForContext<EsSubscription>();
        private readonly IEventStoreConnection _connection;
        private readonly IList<ClassifiedAdDetailsViewModel> _items;

        private EventStoreAllCatchUpSubscription _subscription;

        public EsSubscription(IEventStoreConnection connection,
        IList<ClassifiedAdDetailsViewModel> items)
        {
            _connection = connection;
            _items = items;
        }

        public void Start()
        {
            var settings = new CatchUpSubscriptionSettings(
                2000,
                500,
                Log.IsEnabled(LogEventLevel.Verbose),
                true,
                "try-out-subscription");
            _subscription = _connection.SubscribeToAllFrom(Position.Start, settings, EventAppeared);
        }

        private Task EventAppeared(EventStoreCatchUpSubscription subscription,ResolvedEvent resolvedEvent)
        {
            if(resolvedEvent.Event.EventType.StartsWith("$"))
                return Task.CompletedTask;
            var @event = resolvedEvent.Deserialize();
            Log.Debug($"Projecting event {@event.GetType().Name}");
            switch(@event)
            {
                case ClassifiedAdCreated e:
                    _items.Add(new ClassifiedAdDetailsViewModel
                    {
                        ClassifiedAdId = e.Id
                    });
                    break;
                case ClassifiedAdTitleChanged e:
                    UpdateItem(e.Id, ad => ad.Title = e.Title);
                    break;
                case ClassifiedAdTextUpdated e:
                    UpdateItem(e.Id, ad => ad.Description = e.Text);
                    break;
                case ClassifiedAdPriceUpdated e:
                    UpdateItem(e.Id, ad =>
                    {
                        ad.Price = e.Price;
                        ad.CurrencyCode = e.CurrencyCode;
                    });
                    break;

            }
            return Task.CompletedTask;
        }

        private void UpdateItem(Guid id,Action<ClassifiedAdDetailsViewModel> update)
        {
            var item = _items.FirstOrDefault(x => x.ClassifiedAdId == id);
            if(item==null)return;
            update(item);
        }

        public void Stop() => _subscription.Stop();
    }
}