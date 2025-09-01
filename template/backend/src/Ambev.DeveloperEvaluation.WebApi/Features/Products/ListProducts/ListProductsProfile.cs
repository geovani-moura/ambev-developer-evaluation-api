using Ambev.DeveloperEvaluation.Application.Products.ListProducts;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts;

public class ListProductsProfile : Profile
{
    public ListProductsProfile()
    {
        // WebAPi -> Aplication
        CreateMap<ListProductsRequest, ListProductsCommand>();

        // Aplication -> WebApi
        CreateMap<ListProductsResult, ListProductsResponse>();
    }
}
