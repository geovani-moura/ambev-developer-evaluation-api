using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleProfile : Profile
{
    public UpdateSaleProfile()
    {
        // Application ->  Domain
        CreateMap<UpdateSaleCommand, Sale>();
        CreateMap<UpdateSaleItemCommand, SaleItem>();

        // Domain -> Application
        CreateMap<Sale, UpdateSaleResult>();
        CreateMap<SaleItem, UpdateSaleResult.SaleItemResult>();
    }
}
