using Ambev.DeveloperEvaluation.Application.Users.ListUsers;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.ListUsers;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users;

/// <summary>
/// AutoMapper profile for mapping between ListUsers application results and WebApi responses.
/// </summary>
public class ListUsersProfile : Profile
{
    public ListUsersProfile()
    {
        // Application => WebApi
        CreateMap<ListUsersResult.Item, ListUsersResponse.Item>();
        CreateMap<ListUsersResult, ListUsersResponse>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data))
            .ForMember(dest => dest.TotalItems, opt => opt.MapFrom(src => src.TotalItems))
            .ForMember(dest => dest.CurrentPage, opt => opt.MapFrom(src => src.CurrentPage))
            .ForMember(dest => dest.TotalPages, opt => opt.MapFrom(src => src.TotalPages));

        // WebApi => Application
        CreateMap<ListUsersRequest, ListUsersCommand>();
    }
}
