using Application.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Aggregates
{
    public abstract class AggregateRoot
    {
        private readonly List<EventBase> _changes = [];

        public int Id { get; protected set; }

        public int Version { get; set; } = -1;

        private void ApplyChange(EventBase @event, bool isNew = true)
        {
            var method = this.GetType().GetMethod("Apply", [@event.GetType()]);

            if (method == null)
            {
                throw new ArgumentNullException(nameof(method), $"The Apply method was not found in the Aggeregate for {@event.GetType().Name}.");
            }

            method.Invoke(this, [@event]);

            if (isNew)
            {
                _changes.Add(@event);
            }
        }

        public IEnumerable<EventBase> GetUncommittedChanges() => _changes;

        public void MarkChangesAsCommitted() => _changes.Clear();

        protected void RaiseEvent(EventBase @event) => ApplyChange(@event, true);

        public void ReplayEvents(IEnumerable<EventBase> events)
        {
            foreach (var @event in events)
            {
                ApplyChange(@event, false);
            }
        }
    }
}
