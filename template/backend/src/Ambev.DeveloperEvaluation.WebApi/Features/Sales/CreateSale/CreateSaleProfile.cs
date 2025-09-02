using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleProfile : Profile
{
    public CreateSaleProfile()
    {
        // WebApi → Application
        CreateMap<CreateSaleRequest, CreateSaleCommand>();
        CreateMap<SaleItemRequest, CreateSaleItemDto>();

        // Application → WebApi
        CreateMap<CreateSaleResult, CreateSaleResponse>();
        CreateMap<CreateSaleItemResult, CreateSaleItemResponse>();
    }
}
