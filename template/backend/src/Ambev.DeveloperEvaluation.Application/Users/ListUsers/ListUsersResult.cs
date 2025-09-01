using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers;

/// <summary>
/// Result returned when listing users with pagination.
/// </summary>
public class ListUsersResult
{
    /// <summary>
    /// List of users returned for the current page.
    /// </summary>
    public List<Item> Data { get; set; } = [];

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

    /// <summary>
    /// Representation of a user item in the list.
    /// </summary>
    public class Item
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public UserStatus Status { get; set; }
        public UserRole Role { get; set; }
    }
}
