using Discount.gRPC.Protos;
using FluentValidation;

namespace Discount.gRPC.Validations
{
    public class CreateDiscountRequestValidator : AbstractValidator<CreateDiscountRequest>
    {
        public CreateDiscountRequestValidator()
        {
            RuleFor(x => x.ProductName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).MaximumLength(500);
            RuleFor(x => x.Amount).GreaterThan(0);
        }
    }
}
