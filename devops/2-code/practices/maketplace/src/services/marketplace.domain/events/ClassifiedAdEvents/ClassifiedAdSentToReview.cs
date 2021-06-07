using System;

namespace marketplace.domain.events.ClassifiedAdEvents
{
    public class ClassifiedAdSentToReview
    {
        public Guid Id { get; set; }

        public ClassifiedAdSentToReview(Guid id)
        {
            Id = id;
        }
    }
}