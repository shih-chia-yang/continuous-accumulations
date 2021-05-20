using System;
using System.Reflection;
namespace marketplace.domain.events
{
    public class ClassifiedAdCreated
    {
        public Guid Id { get; set; }

        public Guid OwnerId { get; set; }
    }
}