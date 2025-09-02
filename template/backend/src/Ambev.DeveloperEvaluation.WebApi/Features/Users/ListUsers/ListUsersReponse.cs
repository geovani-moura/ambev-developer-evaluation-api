using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.ListUsers;

public partial class ListUsersResponse
{
    /// <summary>
    /// Representation of a user in the list.
    /// </summary>
    public class ListUserReponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public UserStatus Status { get; set; }
        public UserRole Role { get; set; }
    }
}
