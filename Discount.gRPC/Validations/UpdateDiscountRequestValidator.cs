using Discount.gRPC.Protos;
using FluentValidation;

namespace Discount.gRPC.Validations
{
    public class UpdateDiscountRequestValidator : AbstractValidator<UpdateDiscountRequest>
    {
        public UpdateDiscountRequestValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
            RuleFor(x => x.ProductName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).MaximumLength(500);
            RuleFor(x => x.Amount).GreaterThan(0);
        }
    }
}
