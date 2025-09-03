using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

public class GetProductProfile : Profile
{
    public GetProductProfile()
    {
        // WebAPi -> Aplication
        CreateMap<Guid, GetProductCommand>()
                .ConstructUsing(id => new GetProductCommand(id));

        // Aplication -> WebApi
        CreateMap<GetProductRatingResult, GetProductRatingResponse>();
        CreateMap<GetProductResult, GetProductResponse>();
    }
}
