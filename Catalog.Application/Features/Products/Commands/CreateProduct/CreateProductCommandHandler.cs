using Catalog.Domain.Entities;
using Marten;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Features.Products.Commands.CreateProduct
{
    public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IDocumentSession _session;

        public CreateProductCommandHandler(IDocumentSession session)
        {
            _session = session;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = new Product { 
               Name = request.Name,
               Description = request.Description,
               Price = request.Price,
               Categories = request.Categories,
               ImageFile = request.ImageFile
            };
            _session.Store(product);
            await _session.SaveChangesAsync();
            return product.ID;
        }
    }
}
