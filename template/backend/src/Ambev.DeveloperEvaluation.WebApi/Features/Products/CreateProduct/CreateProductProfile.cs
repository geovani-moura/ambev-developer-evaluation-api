using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.Common;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

public class CreateProductProfile : Profile
{
    public CreateProductProfile()
    {
        // ===== Requests -> Commands =====
        CreateMap<CreateProductRequest, CreateProductCommand>()
            .ForMember(d => d.RatingRate, o => o.MapFrom(s => s.Rating.Rate))
            .ForMember(d => d.RatingCount, o => o.MapFrom(s => s.Rating.Count));

        // ===== Commands -> Requests =====
        CreateMap<CreateProductCommand, CreateProductRequest>()
            .ForMember(d => d.Rating, o => o.MapFrom(s => new RatingRequest
            {
                Rate = s.RatingRate,
                Count = s.RatingCount
            }));
    }
}
