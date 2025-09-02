using Ambev.DeveloperEvaluation.Common.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales;

public class ListSalesProfile : Profile
{
    public ListSalesProfile()
    {
        // Domain -> Application
        CreateMap<SaleItem, ListSaleItemResult>();
        CreateMap<Sale, ListSaleResult>();
        CreateMap<PagedResult<Sale>, ListSalesResult>()
            .ForMember(dest => dest.Sales, opt => opt.MapFrom(src => src.Data))
            .ForMember(dest => dest.TotalItems, opt => opt.MapFrom(src => src.TotalItems))
            .ForMember(dest => dest.CurrentPage, opt => opt.MapFrom(src => src.CurrentPage))
            .ForMember(dest => dest.TotalPages, opt => opt.MapFrom(src => src.TotalPages));
    }
}
