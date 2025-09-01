using Ambev.DeveloperEvaluation.Application.Products.GetCategories;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetCategories;

public class GetCategoriesProfile : Profile
{
    public GetCategoriesProfile()
    {
        // Aplication -> WebApi
        CreateMap<GetCategoriesResult, GetCategoriesResponse>();
    }
}
