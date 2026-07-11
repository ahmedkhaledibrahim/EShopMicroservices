namespace Ordering.Domain.Abstractions
{
    public abstract class Aggregate<T> : BaseEntity<T>, IAggregate
    {
        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(IDomainEvent domainEvent) {
            _domainEvents.Add(domainEvent);
        }
        public IDomainEvent[] ClearDomainEvents()
        {
            var previouDomainEvents = _domainEvents.ToArray();
            _domainEvents.Clear();
            return previouDomainEvents;
        }
    }
}
