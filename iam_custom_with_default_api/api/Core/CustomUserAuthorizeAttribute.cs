using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplication12.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Security.Claims;

namespace api.Core
{
    // Define a custom attribute that inherits from AuthorizeAttribute
    public class CustomUserAuthorizeAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        // Override the OnAuthorizationAsync method to implement the custom logic
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // Get the action path and name, or any unique identifier for it
            var actionPath = context.HttpContext.Request.Path;
            var actionName = context.ActionDescriptor.DisplayName;
            var db = context.HttpContext.RequestServices.GetRequiredService<ApplicationDbContext>();
            // Retrieve the data from database about this controller or action or pages
            // For example, using Entity Framework Core
           
                // Check if there is anyone restricting access to them or free to access to them
                var restriction =  db.RestrictionsAuth
                .FirstOrDefault(r => r.ActionPath == actionPath && r.ActionName == actionName);

                // If there is a restriction, check if the current user and role meet those requirements
                if (restriction != null)
                {
                // Get the current user and role from the HttpContext
                var user = context.HttpContext.User; ;



                // If the user or role is not allowed, restrict access
               
                // Check This Specefic User Can Access Or Not
                if (restriction.Users.Any(x => x.UserName == user?.Identity?.Name))  
                    {
                        context.Result = new ForbidResult();
                        return;
                    }
                }
            

            // If there is no restriction or the user and role are allowed, do the natural way of authorized attribute
         
        }
    }
}
