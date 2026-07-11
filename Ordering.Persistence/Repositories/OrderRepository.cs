using Microsoft.EntityFrameworkCore;
using Ordering.Application.Abstractions.Persistence;
using Ordering.Domain.Aggregates;
using Ordering.Domain.ValueObjects;
using Ordering.Persistence.Context;

namespace Ordering.Persistence.Repositories
{
    public sealed class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order?> GetByIdAsync(OrderId<Guid> orderId, CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.ID == orderId, cancellationToken);
        }

        public async Task<IReadOnlyList<Order>> GetAsync(
            string? orderName,
            Guid? customerId,
            CancellationToken cancellationToken = default)
        {
            var query = _context.Orders
                .Include(o => o.OrderItems)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(orderName))
                query = query.Where(o => o.OrderName.Value.Contains(orderName));

            if (customerId.HasValue)
                query = query.Where(o => o.CustomerId.Value == customerId.Value);

            return await query
                .OrderByDescending(o => o.ID)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Order order, CancellationToken cancellationToken = default)
        {
            await _context.Orders.AddAsync(order, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Order order, CancellationToken cancellationToken = default)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Order order, CancellationToken cancellationToken = default)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
