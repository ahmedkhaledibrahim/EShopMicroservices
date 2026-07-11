using FluentValidation;

namespace Ordering.Application.Features.Orders.Common
{
    public sealed class AddressDtoValidator : AbstractValidator<AddressDto>
    {
        public AddressDtoValidator()
        {
            RuleFor(x => x.AddressLine).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.State).NotEmpty();
            RuleFor(x => x.PostalCode).NotEmpty();
        }
    }

    public sealed class PaymentDtoValidator : AbstractValidator<PaymentDto>
    {
        public PaymentDtoValidator()
        {
            RuleFor(x => x.CardNumber).NotEmpty();
            RuleFor(x => x.Expiration).NotEmpty();
            RuleFor(x => x.CVV).NotEmpty().Length(3, 4);
            RuleFor(x => x.PaymentMethod).GreaterThanOrEqualTo(0);
        }
    }

    public sealed class OrderItemInputDtoValidator : AbstractValidator<OrderItemInputDto>
    {
        public OrderItemInputDtoValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.Currency).NotEmpty();
            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }
}
