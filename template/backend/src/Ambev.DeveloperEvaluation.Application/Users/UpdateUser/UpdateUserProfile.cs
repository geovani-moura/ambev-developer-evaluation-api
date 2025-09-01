using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Users;

public class UpdateUserProfile : Profile
{
    public UpdateUserProfile()
    {
        // Application -> Domain
        CreateMap<UpdateUserCommand, User>();

        //Domain -> Application
        CreateMap<User, UpdateUserResult>();
    }
}
