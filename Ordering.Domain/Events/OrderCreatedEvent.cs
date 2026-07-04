using Ordering.Domain.Abstractions;
using Ordering.Domain.Aggregates;

namespace Ordering.Domain.Events
{
    public record OrderCreatedEvent(Order order) : IDomainEvent
    {
    }
}
