using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users;

/// <summary>
/// AutoMapper profile for mapping between UpdateUser application layer and WebApi layer.
/// </summary>
public class UpdateUserProfile : Profile
{
    public UpdateUserProfile()
    {
        // WebApi -> Application
        CreateMap<UpdateUserRequest, UpdateUserCommand>();

        // Application -> WebApi
        CreateMap<UpdateUserResult, UpdateUserResponse>();
    }
}
