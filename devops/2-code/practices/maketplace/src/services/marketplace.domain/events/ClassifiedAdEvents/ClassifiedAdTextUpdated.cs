using System;

namespace marketplace.domain.events.ClassifiedAdEvents
{
    public class ClassifiedAdTextUpdated
    {
        public Guid Id { get; set; }
        public string Text { get; set; }

        public ClassifiedAdTextUpdated(Guid id,string text)
        {
            Id = id;
            Text = text;
        }
    }
}