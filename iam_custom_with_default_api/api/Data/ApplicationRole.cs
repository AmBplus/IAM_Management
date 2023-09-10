using Microsoft.AspNetCore.Identity;

namespace api.Data
{
    public class ApplicationRole:IdentityRole
    {
        public ICollection<AccessRole> AccessRoles { get; set; }
    }
}
