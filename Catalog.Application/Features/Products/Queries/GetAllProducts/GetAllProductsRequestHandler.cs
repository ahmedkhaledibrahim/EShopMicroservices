using Catalog.Domain.Entities;
using Marten;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsRequestHandler : IRequestHandler<GetAllProductsRequest, GetAllProductsResponse>
    {
        private readonly IDocumentSession _documentSession;

        public GetAllProductsRequestHandler(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public async Task<GetAllProductsResponse> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
        {
            var products = await _documentSession.Query<Product>().ToListAsync();

            return new GetAllProductsResponse()
            {
                Products = products
            };
        }
    }
}
