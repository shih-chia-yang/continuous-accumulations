using System;
using System.Threading.Tasks;
using System.Linq;
using EventStore.ClientAPI;
using System.Text;
using Newtonsoft.Json;

namespace marketplace.api.Infrastructure
{
    public static class EventStoreExtensions
    {
        public static Task AppendEvents(this IEventStoreConnection connection,
        string streamName,long version,
        params object[] events)
        {
            if(events==null || !events.Any()) 
                return Task.CompletedTask;
            var preparedEvents = events
            .Select( @event=>new EventData(
                eventId:Guid.NewGuid(),
                type:@event.GetType().Name,
                isJson:true,
                data: Serialize(@event),
                metadata:Serialize(new EventMetadata{
                    CLRType=@event.GetType().AssemblyQualifiedName
                })
            )).ToArray();
            return connection.AppendToStreamAsync(streamName, version, preparedEvents);
        }

        private static byte[] Serialize(object data)
            => Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
    }
}