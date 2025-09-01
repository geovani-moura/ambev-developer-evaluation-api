using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

public class UpdateProductProfile : Profile
{
    public UpdateProductProfile()
    {
        // ===== WebAPi -> Aplication =====
        CreateMap<UpdateProductRequest, UpdateProductCommand>()
            .ForMember(d => d.RatingRate, o => o.MapFrom(s => s.Rating.Rate))
            .ForMember(d => d.RatingCount, o => o.MapFrom(s => s.Rating.Count));

        // ===== Application -> WebApi =====
        CreateMap<UpdateProductResult, UpdateProductResponse>();
    }
}
