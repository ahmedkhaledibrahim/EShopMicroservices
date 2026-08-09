using FluentValidation;

namespace Basket.Api.Features.Commands.BasketCheckout
{
    public class BasketCheckoutValidator : AbstractValidator<BasketCheckoutRequest>
    {
        public BasketCheckoutValidator()
        {
            RuleFor(x => x.Username)
           .NotEmpty()
           .WithMessage("Username is required.")
           .MaximumLength(100);

            RuleFor(x => x.CustomerId)
                .NotEmpty()
                .WithMessage("Customer id is required.");

            RuleFor(x => x.TotalPrice)
                .GreaterThan(0)
                .WithMessage("Total price must be greater than zero.");

            RuleFor(x => x.AddressLine)
                .NotEmpty()
                .WithMessage("Address line is required.")
                .MaximumLength(200);

            RuleFor(x => x.City)
                .NotEmpty()
                .WithMessage("City is required.")
                .MaximumLength(100);

            RuleFor(x => x.State)
                .NotEmpty()
                .WithMessage("State is required.")
                .MaximumLength(100);

            RuleFor(x => x.PostalCode)
                .NotEmpty()
                .WithMessage("Postal code is required.")
                .MaximumLength(20);

            RuleFor(x => x.CardName)
                .MaximumLength(100)
                .When(x => !string.IsNullOrWhiteSpace(x.CardName));

            RuleFor(x => x.CardNumber)
                .NotEmpty()
                .WithMessage("Card number is required.")
                .CreditCard()
                .WithMessage("Invalid card number.");

            RuleFor(x => x.Expiration)
                .NotEmpty()
                .WithMessage("Expiration date is required.")
                .Matches(@"^(0[1-9]|1[0-2])\/\d{2}$")
                .WithMessage("Expiration must be in MM/YY format.");

            RuleFor(x => x.CVV)
                .NotEmpty()
                .WithMessage("CVV is required.")
                .Matches(@"^\d{3,4}$")
                .WithMessage("CVV must contain 3 or 4 digits.");

            RuleFor(x => x.PaymentMethod)
                .InclusiveBetween(0, 1)
                .WithMessage("Invalid payment method.");
        }
    }
}
