using System.Runtime.Intrinsics;
using System;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using marketplace.domain.events.ClassifiedAdEvents;

namespace marketplace.api.Infrastructure.Projections
{
    public class ClassifiedAdUpcasters : IProjection
    {
        private readonly IEventStoreConnection _connection;
        private readonly Func<Guid, string> _getUserPhoto;
        private const string StreamName="UpcastedClassifiedAdEvent";

        public ClassifiedAdUpcasters(
            IEventStoreConnection connection,
            Func<Guid,string> getUserPhoto)
        {
            _connection = connection;
            _getUserPhoto = getUserPhoto;
        }
        public async Task Project(object @event)
        {
            switch(@event)
            {
                case ClassifiedAdPublished e:
                    var photoUrl = _getUserPhoto(e.OwnerId);
                    var newEvent = new ClassifiedAdUpcastedEvents.V1.ClassifiedAdPublished
                    {
                        Id = e.Id,
                        OwnerId=e.OwnerId, 
                        SellerPhotoUrl=photoUrl,
                        ApprovedBy=e.ApprovedBy};
            await _connection.AppendEvents(StreamName, ExpectedVersion.Any, newEvent);
                    break;
            }
        }
    }
}