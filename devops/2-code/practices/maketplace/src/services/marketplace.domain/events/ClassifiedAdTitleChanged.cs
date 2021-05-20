using System;

namespace marketplace.domain.events
{
    public class ClassifiedAdTitleChanged
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
    }
}