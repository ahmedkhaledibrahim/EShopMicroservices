using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ordering.Domain.Abstractions;

namespace Ordering.Persistence.Interceptors
{
    public class DispatchDomainEventsInterceptor : SaveChangesInterceptor
    {
        private readonly IMediator _mediator;

        public DispatchDomainEventsInterceptor(IMediator mediator)
        {
            _mediator = mediator;
        }
    
        override public async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;
            if (context == null) return await base.SavingChangesAsync(eventData, result, cancellationToken);
            var allDomainEntities = context.ChangeTracker.Entries<IAggregate>();
            var domainEntities = context.ChangeTracker.Entries<IAggregate>()
                .Where(x =>  x.Entity.DomainEvents.Any())
                .Select(x => x.Entity)
                .ToList();
            foreach (var entity in domainEntities)
            {
                var events = entity.DomainEvents.ToList();
                entity.ClearDomainEvents();
                foreach (var domainEvent in events)
                {
                    await _mediator.Publish(domainEvent);
                }
            }
            return base.SavingChanges(eventData, result);
        }
    }
}
