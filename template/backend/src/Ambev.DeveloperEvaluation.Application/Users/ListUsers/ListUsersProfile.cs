using Ambev.DeveloperEvaluation.Application.Users.ListUsers;
using Ambev.DeveloperEvaluation.Common.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Users;

/// <summary>
/// AutoMapper profile for mapping between User entities and ListUsers results.
/// </summary>
public class ListUsersProfile : Profile
{
    public ListUsersProfile()
    {
        // Domain -> Application
        CreateMap<User, ListUsersResult.Item>();

        CreateMap<PagedResult<User>, ListUsersResult>()
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data))
            .ForMember(dest => dest.TotalItems, opt => opt.MapFrom(src => src.TotalItems))
            .ForMember(dest => dest.CurrentPage, opt => opt.MapFrom(src => src.CurrentPage))
            .ForMember(dest => dest.TotalPages, opt => opt.MapFrom(src => src.TotalPages));
    }
}
