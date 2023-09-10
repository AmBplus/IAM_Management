using api.Data;
using Base.Shared.ResultUtility;
using Microsoft.AspNetCore.Identity;
using WebApplication12.Areas.Identity.Data;

namespace api.Services.AdminServices
{
    public class AddRoll
    {
        private UserManager<ApplicationUser> _userManager { get; set; }
        private RoleManager<ApplicationRole> _roleManager { get; set; }

        public AddRoll(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

      

        // public Task<ResultOperation<int>> Check(CancellationToken cancellationToken)
        // {
        //     var Finduser = _userManager.getUse
        // }
    }
}
