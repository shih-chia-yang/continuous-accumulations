using System;
using System.Collections.Generic;
using System.Linq;

namespace marketplace.domain.kernal
{
    public abstract class AggregateRoot:IInternalEventHandler
    {
        public Guid Id { get; protected set; }

        protected abstract void When(object @event);

        public IEnumerable<object> GetChanges() => _changes.AsEnumerable();
        private readonly List<object> _changes;

        protected AggregateRoot()
        {
            _changes = new List<object>();
        }

        protected void Apply(object @event)
        {
            When(@event);
            EnsureValidState();
            _changes.Add(@event);
        }

        public void ClearChanges() => _changes.Clear();

        protected abstract void EnsureValidState();

        protected void ApplyToEntity(IInternalEventHandler entity, object @event) => entity?.Handle(@event);
        public void Handle(object @event) => When(@event);
    }
}