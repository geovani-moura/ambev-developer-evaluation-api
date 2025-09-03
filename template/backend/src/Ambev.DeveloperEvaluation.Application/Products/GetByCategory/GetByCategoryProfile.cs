using Ambev.DeveloperEvaluation.Common.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.GetByCategory;

public class GetByCategoryProfile : Profile
{
    public GetByCategoryProfile()
    {
        CreateMap<Product, GetByCategoryProductResult>()
            .ForPath(x => x.Rating.Rate, o => o.MapFrom(x => x.RatingRate))
            .ForPath(x => x.Rating.Count, o => o.MapFrom(x => x.RatingCount));

        CreateMap<PagedResult<Product>, GetByCategoryResult>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data))
            .ForMember(dest => dest.TotalItems, opt => opt.MapFrom(src => src.TotalItems))
            .ForMember(dest => dest.CurrentPage, opt => opt.MapFrom(src => src.CurrentPage))
            .ForMember(dest => dest.TotalPages, opt => opt.MapFrom(src => src.TotalPages));
    }
}
