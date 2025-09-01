using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

public class GetProductProfile : Profile
{
    public GetProductProfile()
    {
        CreateMap<Product, GetProductResult>()
            .ForPath(x => x.Rating.Rate, o => o.MapFrom(x => x.RatingRate))
            .ForPath(x => x.Rating.Count, o => o.MapFrom(x => x.RatingCount));
    }
}
