using Ambev.DeveloperEvaluation.Application.Products.GetByCategory;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetByCategory;

public class GetByCategoryProfile : Profile
{
    public GetByCategoryProfile()
    {
        CreateMap<GetByCategoryResult, GetByCategoryResponse>();
    }
}
