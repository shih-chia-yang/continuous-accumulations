using System;

namespace marketplace.domain.events.ClassifiedAdEvents
{
    public class ClassifiedAdTitleChanged
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public ClassifiedAdTitleChanged(Guid id,string title)
        {
            Id = id;
            Title = title;
        }
    }
}