using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Features.Products.Queries.GetProductByID
{
    public sealed class GetProductByIDRequest : IRequest<GetProductByIDResponse>
    {
        [Required]
        public Guid ID { get; set; }
    }
}
