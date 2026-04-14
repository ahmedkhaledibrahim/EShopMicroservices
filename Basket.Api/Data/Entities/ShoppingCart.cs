using Marten.Schema;
using Microsoft.AspNetCore.Http.Features;

namespace Basket.Api.Data.Entities
{
    public class ShoppingCart
    {
        [Identity]
        public string Username { get; set; }
        public List<ShoppingCartItem> Items { get; set; }
        public decimal TotalPrice => Items.Sum(i => i.Price * i.Quantity);

    }
}
