using System;

namespace marketplace.domain.events
{
    public class ClassifiedAdTextUpdated
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
    }
}