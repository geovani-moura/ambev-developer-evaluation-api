using Ambev.DeveloperEvaluation.Domain.Enums;
using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser;

/// <summary>
/// Request body for updating an existing user.
/// </summary>
public class UpdateUserRequest
{
    /// <summary>
    /// User ID to update (from route).
    /// </summary>
    [JsonIgnore]
    public Guid Id { get; set; }

    /// <summary>
    /// Username of the user.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Password of the user.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Phone number of the user.
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Email address of the user.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Current status of the user (Active, Inactive, Suspended).
    /// </summary>
    public UserStatus Status { get; set; }

    /// <summary>
    /// Role of the user (Customer, Manager, Admin).
    /// </summary>
    public UserRole Role { get; set; }
}
