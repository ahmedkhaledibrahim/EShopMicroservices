using FluentValidation;
using Ordering.Application.Features.Orders.Common;

namespace Ordering.Application.Features.Orders.Commands.CreateOrder
{
    public sealed class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty();

            RuleFor(x => x.OrderName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.ShippingAddress)
                .NotNull()
                .SetValidator(new AddressDtoValidator());

            RuleFor(x => x.BillingAddress)
                .NotNull()
                .SetValidator(new AddressDtoValidator());

            RuleFor(x => x.Payment)
                .NotNull()
                .SetValidator(new PaymentDtoValidator());

            RuleFor(x => x.OrderItems)
                .NotEmpty()
                .WithMessage("Order must contain at least one item.");

            RuleForEach(x => x.OrderItems)
                .SetValidator(new OrderItemInputDtoValidator());
        }
    }
}
