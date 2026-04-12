using Catalog.Domain.Entities;
using Marten;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Catalog.Application.Common.SystemExceptions;

namespace Catalog.Application.Features.Products.Commands.DeleteProduct
{
    public sealed class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IDocumentSession _session;

        public DeleteProductHandler(IDocumentSession session)
        {
            _session = session;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _session.LoadAsync<Product>(request.ID);
            if (product == null) throw new EntityNotFoundException("Product with " + request.ID + " not found.");
            _session.Delete(product);
            await _session.SaveChangesAsync();
            return true;
        }
    }
}
