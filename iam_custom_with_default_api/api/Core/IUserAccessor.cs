using System.Security.Claims;

namespace api.Core;

/// <summary>
/// Abstraction to access the current user
/// </summary>
public interface IUserAccessor
{
    /// <summary>
    /// Gets current user
    /// </summary>
    ClaimsPrincipal? User { get; }
}