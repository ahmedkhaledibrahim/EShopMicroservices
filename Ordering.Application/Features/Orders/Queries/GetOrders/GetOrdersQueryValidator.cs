using FluentValidation;

namespace Ordering.Application.Features.Orders.Queries.GetOrders
{
    public sealed class GetOrdersQueryValidator : AbstractValidator<GetOrdersQuery>
    {
        public GetOrdersQueryValidator()
        {
            RuleFor(x => x.OrderName)
                .MaximumLength(100)
                .When(x => !string.IsNullOrWhiteSpace(x.OrderName));
        }
    }
}
