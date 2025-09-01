using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

public class CreateProductProfile : Profile
{
    public CreateProductProfile()
    {
        CreateMap<CreateProductCommand, Product>()
            .ForMember(d => d.RatingRate, o => o.MapFrom(s => s.RatingRate))
            .ForMember(d => d.RatingCount, o => o.MapFrom(s => s.RatingCount));

        CreateMap<Product, CreateProductResult>()
            .ForPath(d => d.Rating.Rate, o => o.MapFrom(s => s.RatingRate))
            .ForPath(d => d.Rating.Count, o => o.MapFrom(s => s.RatingCount));
    }
}
