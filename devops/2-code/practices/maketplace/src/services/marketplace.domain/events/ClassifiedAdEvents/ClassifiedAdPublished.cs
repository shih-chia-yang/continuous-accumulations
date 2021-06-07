using System;

namespace marketplace.domain.events.ClassifiedAdEvents
{
    public class ClassifiedAdPublished
    {
        public Guid Id { get; set; }
        public Guid ApprovedBy { get; set; }

        public ClassifiedAdPublished(Guid id,Guid approveBy)
        {
            Id = id;
            ApprovedBy = approveBy;
        }
    }
}