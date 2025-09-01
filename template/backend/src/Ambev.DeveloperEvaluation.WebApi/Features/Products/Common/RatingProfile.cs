using Ambev.DeveloperEvaluation.Application.Products.Common;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.Common;

public class RatingProfile : Profile
{
    public RatingProfile()
    {
        // Aplication -> WebApi
        CreateMap<RatingResult, RatingResponse>();


        CreateMap<RatingResponse, RatingResult>();
    }
}
