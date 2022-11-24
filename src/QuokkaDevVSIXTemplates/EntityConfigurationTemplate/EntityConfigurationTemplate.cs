using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace $rootnamespace$
{
    public class $safeitemname$Configuration : IEntityTypeConfiguration<$safeitemname$>
    {
        public void Configure(EntityTypeBuilder<$safeitemname$> builder)
        {
            builder.ToTable("MyEntityTableName");
        }
    }
}
