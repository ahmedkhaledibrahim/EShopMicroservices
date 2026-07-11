using FluentValidation;
using Ordering.Application.Features.Orders.Common;

namespace Ordering.Application.Features.Orders.Commands.AddOrderItem
{
    public sealed class AddOrderItemCommandValidator : AbstractValidator<AddOrderItemCommand>
    {
        public AddOrderItemCommandValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty();
            RuleFor(x => x.Item).NotNull().SetValidator(new OrderItemInputDtoValidator());
        }
    }
}
