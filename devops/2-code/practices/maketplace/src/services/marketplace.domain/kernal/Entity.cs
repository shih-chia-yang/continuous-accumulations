using System.Collections.Generic;
using System.Linq;

namespace marketplace.domain.kernal
{
    public abstract class Entity
    {
        private readonly List<object> _events;

        protected Entity() => _events = new List<object>();
        public IEnumerable<object> GetChanges() => _events.AsEnumerable();
        public void ClearChanges()=>_events.Clear();

        protected void Apply(object @event)
        {
            When(@event);
            EnsureValidState();
            _events.Add(@event);
        }

        protected abstract void When(object @event);

        protected abstract void EnsureValidState();
    }
}