using Catalog.Domain.Entities;
using Marten;
using MediatR;


namespace Catalog.Application.Features.Products.Queries.GetProductByID
{
    public sealed class GetProductByIDHandler : IRequestHandler<GetProductByIDRequest, GetProductByIDResponse>
    {
        private readonly IQuerySession _querySession;
        public GetProductByIDHandler(IQuerySession querySession)
        {
            _querySession = querySession;
        }
        public async Task<GetProductByIDResponse> Handle(GetProductByIDRequest request, CancellationToken cancellationToken)
        {
            var product = await _querySession.Query<Product>().FirstOrDefaultAsync(x => x.ID == request.ID);
            if (product == null) return null;
            return new GetProductByIDResponse { ID = product.ID, Name = product.Name, Description = product.Description, Price = product.Price, Categories = product.Categories, ImageFile = product.ImageFile };
        
        }
    }
}
