using Base.Shared.ResultUtility;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace api.Services;
    // Request model
    public class RefreshTokenRequest : IRequest<ResultOperation<AuthResponse>>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime AccessTokenExpireAt { get; set; }   
        public DateTime RefreshTokenExpireAt { get; set; }
    }

// Handler 
public class RefreshTokenHandler : IRequestHandler<RefreshTokenRequest, AuthResponse>
{
    private readonly UserManager<IdentityUser> userManager;
    private readonly IJwtTokenService jwtService;

    public RefreshTokenHandler(UserManager<IdentityUser> userManager,
                               IJwtTokenService jwtService)
    {
        this.userManager = userManager;
        this.jwtService = jwtService;
    }

    public async Task<AuthResponse> Handle(RefreshTokenRequest request,
                                           CancellationToken cancellationToken)
    {
        // Validate access token
        var principal = jwtService.GetPrincipalFromExpiredToken(request.AccessToken);

        // Get user and validate refresh token
        var user = await userManager.FindByNameAsync(principal.Identity.Name);

        if (user.RefreshToken != request.RefreshToken ||
            user.RefreshTokenExpired < DateTime.UtcNow)
        {
            return null;
        }

        // Generate new tokens
        var tokens = jwtService.GenerateTokens(principal.Claims);

        // Update user's refresh token
        user.RefreshToken = tokens.RefreshToken;
        user.RefreshTokenExpired = DateTime.UtcNow.AddDays(7);

        await userManager.UpdateAsync(user);

        return tokens;
    }
}
