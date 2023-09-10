using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Mapping
{
    public class AccessRoleMapping: IEntityTypeConfiguration<AccessRole>
    {
        public void Configure(EntityTypeBuilder<AccessRole> builder)
        {
            builder.HasKey(x=>x.Guid);
            builder.Property(x=>x.AccessRoleName).IsRequired().HasMaxLength(200);
            builder.HasOne(x => x.ApplicationRole).WithMany(x => x.AccessRoles).HasForeignKey(x => x.ApplicationRoleId);
        }
    }
}
