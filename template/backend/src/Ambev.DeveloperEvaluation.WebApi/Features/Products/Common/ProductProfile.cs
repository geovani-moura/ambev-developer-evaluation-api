using Ambev.DeveloperEvaluation.Application.Products.Common;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.Common;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        // Aplication -> WebApi  
        CreateMap<ProductResult, ProductResponse>();
    }
}
