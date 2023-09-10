using api.Core;
using Base.Shared.ResultUtility;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApplication12.Areas.Identity.Data;

namespace api.Services
{
    public interface ITokenService
    {
        public JwtSecurityToken GetToken(List<Claim> authClaims);
        public string GenerateRefreshToken();
   
    }
    public interface IRefreshTokenService
    {
        jWTTokenRefreshTokenResult GetNew(string refreshToken);
    }
    public class RefreshTokenService : IRefreshTokenService
    {
        public RefreshTokenService(UserManager<ApplicationUser> userManager, IUserAccessor userAccessor)
        {
            UserManager = userManager;
            UserAccessor = userAccessor;
        }

        public UserManager<ApplicationUser> UserManager { get; }
        public IUserAccessor UserAccessor { get; }

        public jWTTokenRefreshTokenResult GetNew(string refreshToken)
        {
            var userName = UserAccessor?.User?.Identity?.Name;
            var user = UserManager.Users.FirstOrDefault(x => x.UserName == userName);
            if(user.RefreshTokenExpireTime> DateTime.Now && user.RefreshToken == refreshToken)
            {
             return   GenerateNewRefreshTokenAndToken(user);
            }
            throw new Exception();
        }

        private jWTTokenRefreshTokenResult GenerateNewRefreshTokenAndToken(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
    }
    public record jWTTokenRefreshTokenResult
    {
        string Token { get; set; }    
        public string RefreshToken { get; set; }    
        public string RefreshTokenExipireTime { get; set; }
        public string TokenExpireTime { get; set; }
    }
    public class TokenServices : ITokenService
    {
   
        private readonly IConfiguration _configuration;

        public TokenServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
 