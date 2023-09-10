using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.ConfigModel
{
    public class RestrictionAuthConfiguration : IEntityTypeConfiguration<RestrictionAuth>
    {
        public void Configure(EntityTypeBuilder<RestrictionAuth> builder)
        {
            builder.HasIndex(x => x.ActionName);
            builder.HasIndex(x => x.ActionPath);
        }
    }
}
