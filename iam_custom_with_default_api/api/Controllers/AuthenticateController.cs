using api.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication12.Areas.Identity.Data;

namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticateController : ApiBaseController
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly IMediator _mediator;
    public AuthenticateController(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration,
        IMediator mediator)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _mediator = mediator;
    }

    // [HttpPost]
    // [Route("login")]
    // public async Task<IActionResult> Login([FromBody] LoginUserCommandRequest model)
    // {
    //  
    // }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommandRequest model)
    {
      var result =  await    _mediator.Send(model);
        return MapToApiResult(result);
    }

    //[HttpPost]
    //[Route("register-admin")]
    //public async Task<IActionResult> RegisterAdmin([FromBody] RegisterCommandRequest model)
    //{
    //    var userExists = await _userManager.FindByNameAsync(model.Username);
    //    if (userExists != null)
    //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

    //    var user = CreateUser(model.Email, Guid.NewGuid().ToString(), model.Username);
      
    //    var result = await _userManager.CreateAsync(user, model.Password);
    //    if (!result.Succeeded)
    //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

    //    if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
    //        await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
    //    if (!await _roleManager.RoleExistsAsync(UserRoles.User))
    //        await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

    //    if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
    //    {
    //        await _userManager.AddToRoleAsync(user, UserRoles.Admin);
    //    }
    //    if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
    //    {
    //        await _userManager.AddToRoleAsync(user, UserRoles.User);
    //    }
    //    return Ok(new Response { Status = "Success", Message = "User created successfully!" });
    //}

   


 
  


}
