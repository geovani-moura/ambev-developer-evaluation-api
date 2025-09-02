using Ambev.DeveloperEvaluation.Common.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IUserRepository using Entity Framework Core
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of UserRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public UserRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new user in the database
    /// </summary>
    /// <param name="user">The user to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user</returns>
    public async Task<User> CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return user;
    }

    /// <summary>
    /// Retrieves a user by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the user</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Users.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    /// <summary>
    /// Retrieves a user by their email address
    /// </summary>
    /// <param name="email">The email address to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    /// <summary>
    /// Deletes a user from the database
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the user was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await GetByIdAsync(id, cancellationToken);
        if (user == null)
            return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<User?> UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        var existing = await GetByIdAsync(user.Id, cancellationToken);
        if (existing == null)
            return null;

        _context.Entry(existing).CurrentValues.SetValues(user);
        await _context.SaveChangesAsync(cancellationToken);
        return existing;
    }

    public async Task<PagedResult<User>> ListAsync(
        int page = 1,
        int size = 10,
        string? order = null,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Users.AsQueryable();

        query = ApplyOrdering(query, order);

        var totalItems = await query.CountAsync(cancellationToken);
        var users = await query
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(cancellationToken);

        return new PagedResult<User>
        {
            Data = users,
            TotalItems = totalItems,
            CurrentPage = page,
            TotalPages = (int)Math.Ceiling(totalItems / (double)size)
        };
    }


    // --- Helper de ordenação minimalista ---
    private static IQueryable<User> ApplyOrdering(IQueryable<User> q, string? order)
    {
        if (string.IsNullOrWhiteSpace(order))
            return q.OrderBy(u => u.Id);

        IOrderedQueryable<User>? ordered = null;

        foreach (var raw in order.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            var parts = raw.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var field = parts[0].ToLowerInvariant();
            var desc = parts.Length > 1 && parts[1].Equals("desc", StringComparison.OrdinalIgnoreCase);

            Expression<Func<User, object>> key = field switch
            {
                "id" => u => u.Id,
                "email" => u => u.Email!,
                "username" => u => u.Username!,
                "status" => u => u.Status!,
                "role" => u => u.Role!,
                "phone" => u => u.Phone!,
                _ => u => u.Id
            };

            if (ordered == null)
            {
                ordered = desc ? q.OrderByDescending(key) : q.OrderBy(key);
            }
            else
            {
                ordered = desc ? ordered.ThenByDescending(key) : ordered.ThenBy(key);
            }
        }

        return ordered ?? q.OrderBy(u => u.Id);
    }
}
