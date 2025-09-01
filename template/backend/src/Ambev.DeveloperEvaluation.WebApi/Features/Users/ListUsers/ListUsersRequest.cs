namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.ListUsers;

/// <summary>
/// Request parameters for listing users with pagination and ordering.
/// </summary>
public class ListUsersRequest
{
    /// <summary>
    /// Page number (default: 1).
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Number of items per page (default: 10).
    /// </summary>
    public int Size { get; set; } = 10;

    /// <summary>
    /// Ordering string. 
    /// Example: "username asc, email desc".  
    /// Allowed fields: id, username, email, status, role.
    /// </summary>
    public string? Order { get; set; }
}
