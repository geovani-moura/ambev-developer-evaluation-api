namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.ListUsers;

/// <summary>
/// Response returned when listing users.
/// </summary>
public partial class ListUsersResponse
{
    /// <summary>
    /// List of users for the current page.
    /// </summary>
    public List<ListUserReponse> Data { get; set; } = [];

    /// <summary>
    /// Total number of users available.
    /// </summary>
    public int TotalItems { get; set; }

    /// <summary>
    /// Current page number.
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// Total number of pages.
    /// </summary>
    public int TotalPages { get; set; }
}
