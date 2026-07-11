using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.RemoveOrderItem
{
    public sealed class RemoveOrderItemCommandValidator : AbstractValidator<RemoveOrderItemCommand>
    {
        public RemoveOrderItemCommandValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty();
            RuleFor(x => x.ProductId).NotEmpty();
        }
    }
}
