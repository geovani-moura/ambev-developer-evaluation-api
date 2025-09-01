using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers;

/// <summary>
/// Command for listing users with pagination and ordering.
/// </summary>
public class ListUsersCommand : IRequest<ListUsersResult>
{
    /// <summary>
    /// Current page number (default: 1).
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Number of items per page (default: 10).
    /// </summary>
    public int Size { get; set; } = 10;

    /// <summary>
    /// Ordering string.  
    /// Example: "username asc, email desc".  
    /// Supported fields: id, username, email, status, role.
    /// </summary>
    public string? Order { get; set; }
}
