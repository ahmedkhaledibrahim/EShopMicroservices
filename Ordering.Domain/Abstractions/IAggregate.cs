namespace Ordering.Domain.Abstractions
{
    public interface IAggregate<T> : IEntity<T>
    {
        IReadOnlyList<IDomainEvent> DomainEvents { get; }
        IDomainEvent[] ClearDomainEvents();
    }
}
