using Base.Shared.ResultUtility;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebApplication12.Areas.Identity.Data;

namespace api.Services;

public record RegisterCommandRequest : IRequest<ResultOperation<int>>
{
    [Required(ErrorMessage = "User Name is required")]
    public string? Username { get; set; }
    [Required(ErrorMessage = "LastName is required")]
    public string? LastName { get; set; }
    [Required(ErrorMessage = "Name is required")]
    public string? Name { get; set; }

    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }


}


public class RegisterCommandHandler : IRequestHandler<RegisterCommandRequest, ResultOperation<int>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ITokenService _tokenService;
    private readonly JwtConfig _jwtConfig;

    public RegisterCommandHandler(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, ITokenService tokenService)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _tokenService = tokenService;
        _jwtConfig = new JwtConfig();
    }

    public async Task<ResultOperation<int>> Handle(RegisterCommandRequest model, CancellationToken cancellationToken)
    {
        var userExists = await _userManager.FindByNameAsync(model.Username);
        if (userExists != null)
            return ResultOperation<int>.ToFailedResult("User already exists!");
        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, model.Username),
            new Claim(ClaimTypes.Role,UserRoles.User),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        var user = CreateUser(
            model.Email,
            Guid.NewGuid().ToString(),
            model.Username
        );
        var token = _tokenService.GetToken(authClaims);
        var RefreshToken = _tokenService.GenerateRefreshToken();
        user.RefreshToken = RefreshToken;
        user.RefreshTokenExpireTime = token.ValidTo.ToString();
        var result = await _userManager.CreateAsync(user, model.Password);
        var AddClaims = await _userManager.AddClaimsAsync(user, authClaims);
        await _userManager.AddToRoleAsync(user, "Admin");
        if (!result.Succeeded)
            
            return ResultOperation<int>.ToFailedResult(message: "User creation failed! Please check user details and try again.",data:StatusCodes.Status500InternalServerError);

        return ResultOperation<int>.ToSuccessResult(message:"The user successfully added", data: StatusCodes.Status200OK);
    }
    private ApplicationUser CreateUser(string? email, string securityStamp, string username)
    {
        try
        {
            var user = Activator.CreateInstance<ApplicationUser>();
            user.Email = email;
            user.SecurityStamp = securityStamp;
            user.UserName = username;
            return user;
        }
        catch
        {
            throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively ");

        }
    }
}






