using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder
{
    public sealed class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty();
        }
    }
}
