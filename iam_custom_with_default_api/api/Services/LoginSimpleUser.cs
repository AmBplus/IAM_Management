using Base.Shared;
using Base.Shared.ResultUtility;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApplication12.Areas.Identity.Data;

namespace api.Services;
    public record LoginUserCommandRequest : IRequest<ResultOperation<LoginResponse>>
    {
    [Required(ErrorMessage = "User Name is required")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
}
public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, ResultOperation<LoginResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ITokenService _tokenService;
    private readonly JwtConfig _jwtConfig;

    public LoginUserCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
   
    }



    public async Task<ResultOperation<LoginResponse>> Handle(LoginUserCommandRequest model, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(model.Username);
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = _tokenService.GetToken(authClaims);
            var refreshToken = _tokenService.GenerateRefreshToken();
            
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpireTime = _jwtConfig.RefreshTokenExpireTime;
            await _userManager.UpdateAsync(user);
            var loginResponse =
            new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshTokenExpireTime = token.ValidTo,
                RefreshToken = ""
            };
            return loginResponse.ToSuccessResult();
        }
   
        //  return Unauthorized();


    }
}

