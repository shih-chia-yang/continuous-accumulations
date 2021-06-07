using System;
using System.Reflection;
namespace marketplace.domain.events.ClassifiedAdEvents
{
    public class ClassifiedAdCreated
    {
        public Guid Id { get; set; }

        public Guid OwnerId { get; set; }

        public ClassifiedAdCreated(Guid id, Guid ownerId)
        {
            Id = id;
            OwnerId = ownerId;
        }
    }
}