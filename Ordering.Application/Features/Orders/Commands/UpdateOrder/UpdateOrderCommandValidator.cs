using FluentValidation;
using Ordering.Application.Features.Orders.Common;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public sealed class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty();
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.OrderName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.ShippingAddress).NotNull().SetValidator(new AddressDtoValidator());
            RuleFor(x => x.BillingAddress).NotNull().SetValidator(new AddressDtoValidator());
            RuleFor(x => x.Payment).NotNull().SetValidator(new PaymentDtoValidator());
        }
    }
}
