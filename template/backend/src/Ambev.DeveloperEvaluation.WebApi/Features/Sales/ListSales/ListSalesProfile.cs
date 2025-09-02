using Ambev.DeveloperEvaluation.Application.Sales.ListSales;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales;

public class ListSalesProfile : Profile
{
    public ListSalesProfile()
    {
        // Application → WebApi
        CreateMap<ListSaleItemResult, ListSaleItemResponse>();
        CreateMap<ListSaleResult, ListSaleResponse>();
        CreateMap<ListSalesResult, ListSalesResponse>();

        // WebApi => Application
        CreateMap<ListSalesRequest, ListSalesCommand>();
    }
}
