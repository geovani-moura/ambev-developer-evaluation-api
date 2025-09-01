using Ambev.DeveloperEvaluation.Application.Products.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

public class CreateProductProfile : Profile
{
    public CreateProductProfile()
    {
        // ===== Domain -> Aplication =====
        CreateMap<Product, CreateProductCommand>();

        // ===== Aplication -> Domain =====
        CreateMap<CreateProductCommand, Product>();

        CreateMap<Product, CreateProductResult>()
            .ForMember(d => d.Rating, o => o.MapFrom(s => new RatingResult
            {
                Rate = s.RatingRate,
                Count = s.RatingCount
            }));
    }
}