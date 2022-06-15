using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace $rootnamespace$
{
    public class $safeitemname$ : IEntityTypeConfiguration<object>
    {
        public void Configure(EntityTypeBuilder<object> builder)
        {
            builder.ToTable("MyEntityTableName");
        }
    }
}
