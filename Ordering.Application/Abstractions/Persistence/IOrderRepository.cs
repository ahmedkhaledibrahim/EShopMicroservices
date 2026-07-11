using Ordering.Domain.Aggregates;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Abstractions.Persistence
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(OrderId<Guid> orderId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Order>> GetAsync(string? orderName, Guid? customerId, CancellationToken cancellationToken = default);
        Task AddAsync(Order order, CancellationToken cancellationToken = default);
        Task UpdateAsync(Order order, CancellationToken cancellationToken = default);
        Task DeleteAsync(Order order, CancellationToken cancellationToken = default);
    }
}
