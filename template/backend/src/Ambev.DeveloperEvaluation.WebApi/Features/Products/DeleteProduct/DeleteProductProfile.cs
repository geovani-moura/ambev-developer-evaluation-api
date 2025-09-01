using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;

public class DeleteProductProfile : Profile
{
    public DeleteProductProfile()
    {
        // WebAPi -> Aplication
        CreateMap<Guid, DeleteProductCommand>()
                .ConstructUsing(id => new DeleteProductCommand(id));

        // Aplication -> WebApi
        CreateMap<DeleteProductResult, DeleteProductResponse>();
    }
}
