using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebApplication12.Areas.Identity.Data;


public class ApplicationUser : IdentityUser
{
    public string RefreshToken { get; set; }
    public string RefreshTokenExpireTime { get; set; }
}
