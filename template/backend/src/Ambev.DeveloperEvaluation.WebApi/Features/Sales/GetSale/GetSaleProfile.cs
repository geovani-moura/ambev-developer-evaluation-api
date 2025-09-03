using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

public class GetSaleProfile : Profile
{
    public GetSaleProfile()
    {
        // WebAPi -> Aplication
        CreateMap<Guid, GetSaleCommand>()
                .ConstructUsing(id => new GetSaleCommand(id));

        // Aplication -> WebApi
        CreateMap<GetSaleItemResult, GetSaleItemResponse>();
        CreateMap<GetSaleResult, GetSaleResponse>();
    }
}
