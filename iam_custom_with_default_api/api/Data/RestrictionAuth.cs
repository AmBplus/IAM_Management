using System.ComponentModel.DataAnnotations;
using WebApplication12.Areas.Identity.Data;

namespace api.Data
{
    public class RestrictionAuth
    {
        // Use a primary key attribute to identify the restriction
        [Key]
        public int Id { get; set; }

        // Use a string property to store the action path
        public string ActionPath { get; set; }

        // Use a string property to store the action name
        public string ActionName { get; set; }

        // Use a list of strings to store the users who are not allowed to access the action
        public List<ApplicationUser> Users { get; set; }

        // Use a list of strings to store the roles who are not allowed to access the action
        public List<ApplicationRole> Roles { get; set; }
    }

   
}
