using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Features.Products.Commands.DeleteProduct
{
    public sealed class DeleteProductCommand : IRequest<bool>
    {
        [Required(ErrorMessage = "ID is required")]
        public Guid ID { get; init; }
    }
}
