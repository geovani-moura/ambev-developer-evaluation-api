using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales;

public class CreateSaleProfile : Profile
{
    public CreateSaleProfile()
    {
        CreateMap<SaleItem, CreateSaleItemResult>();
        CreateMap<Sale, CreateSaleResult>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        CreateMap<CreateSaleItemDto, SaleItem>();
        CreateMap<CreateSaleCommand, Sale>();
    }
}
