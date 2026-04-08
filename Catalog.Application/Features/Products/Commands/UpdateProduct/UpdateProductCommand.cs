using Catalog.Domain.Entities;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Features.Products.Commands.UpdateProduct
{
    public sealed class UpdateProductCommand : IRequest<bool>
    {
        [Required(ErrorMessage = "ID is required")]
        public Guid ID { get; init; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; init; }
        public string Description { get; init; }
        public HashSet<string> Categories { get; init; }
        public string ImageFile { get; init; }
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; init; }

        
    };

    public static class  UpdateProductCommandExtensions {
        public static void MapToProduct(this UpdateProductCommand request, Product product)
        {
            TypeAdapterConfig<UpdateProductCommand, Product>.NewConfig()
                .Ignore(dest => dest.Categories)
                .AfterMapping((src, dest) => {
                    if(src.Categories.Count > 0) dest.Categories = src.Categories.ToList();
                });
            request.Adapt(product);
        }
    }
    

     
}
