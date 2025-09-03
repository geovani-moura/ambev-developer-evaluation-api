using Ambev.DeveloperEvaluation.Application.Products.GetByCategory;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetByCategory;

public class GetByCategoryProfile : Profile
{
    public GetByCategoryProfile()
    {
        // WebAPi -> Aplication
        CreateMap<GetByCategoryRequest, GetByCategoryCommand>();

        // Aplication -> WebApi
        CreateMap<GetByCategoryRatingResult, GetByCategoryRatingResponse>();
        CreateMap<GetByCategoryProductResult, GetByCategoryProductResponse>();
        CreateMap<GetByCategoryResult, GetByCategoryResponse>();
    }
}
