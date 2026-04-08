
using Catalog.Domain.Entities;
using Mapster;
using Marten;
using Marten.Internal.Sessions;
using MediatR;
using static Catalog.Application.Common.SystemExceptions;

namespace Catalog.Application.Features.Products.Commands.UpdateProduct
{
    public sealed class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IDocumentSession _session;

        public UpdateProductHandler(IDocumentSession session)
        {
            _session = session;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _session.LoadAsync<Product>(request.ID);
            if (product == null) throw new EntityNotFoundException("Product with ID " + request.ID + " not found.");
            request.MapToProduct(product);
            await _session.SaveChangesAsync();
            return true;
        }
    }
}
