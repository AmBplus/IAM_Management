using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class AccessRole
    {
        public string Guid { get; set; }
        public string AccessRoleName { get; set; }
        public string? ApplicationRoleId { get; set; }
        public ApplicationRole ApplicationRole { get; set; }
    }
}
