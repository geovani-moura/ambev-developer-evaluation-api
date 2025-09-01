using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

public class CreateProductProfile : Profile
{
    public CreateProductProfile()
    {
        // WebAPi -> Aplication
        CreateMap<CreateProductRequest, CreateProductCommand>()
            .ForMember(d => d.RatingRate, o => o.MapFrom(s => s.Rating.Rate))
            .ForMember(d => d.RatingCount, o => o.MapFrom(s => s.Rating.Count));

        // Aplication -> WebApi
        CreateMap<CreateProductResult, CreateProductResponse>();
    }
}
