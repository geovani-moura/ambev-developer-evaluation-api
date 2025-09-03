using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;

public class DeleteSaleProfile : Profile
{
    public DeleteSaleProfile()
    {
        // WebAPi -> Aplication
        CreateMap<Guid, DeleteSaleCommand>()
                .ConstructUsing(id => new DeleteSaleCommand(id));

        // Aplication -> WebApi
        CreateMap<DeleteSaleResult, DeleteSaleResponse>();
    }
}
