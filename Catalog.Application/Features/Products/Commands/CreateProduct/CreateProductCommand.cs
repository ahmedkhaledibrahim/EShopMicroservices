using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Features.Products.Commands.CreateProduct
{
    public sealed class CreateProductCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Categories { get; set; }
        public decimal Price { get; set; }
        public string ImageFile { get; set; }
    }
}
