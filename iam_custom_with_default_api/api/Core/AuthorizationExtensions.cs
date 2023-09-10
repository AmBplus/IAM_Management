using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace api.Core;

/// <summary>
/// Authorization extension methods
/// </summary>
public static class AuthorizationExtensions
{
    /// <summary>
    /// Returns true if user is logged in (authenticated).
    /// </summary>
    public static bool IsLoggedIn([NotNullWhen(true)] this IUserAccessor? userAccessor)
    {
        return userAccessor?.User?.Identity?.IsAuthenticated == true;
    }

    /// <summary>
    /// Returns true if user is logged in (authenticated).
    /// </summary>
    public static bool IsLoggedIn([NotNullWhen(true)] this ClaimsPrincipal? user)
    {
        return user?.Identity?.IsAuthenticated == true;
    }


    /// <summary>
    /// Gets name identifier claim from given identity
    /// </summary>
    /// <param name="identity"></param>
    /// <returns></returns>
    public static string? GetIdentifier(this ClaimsPrincipal? identity)
    {
        if (identity == null)
            return null;

        return identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }

  

}