using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace $rootnamespace$
{
    public class $safeitemname$ : IEntityTypeConfiguration<$fileinputname$>
    {
        public void Configure(EntityTypeBuilder<$fileinputname$> builder)
        {
            builder.ToTable(nameof($fileinputname$));
        }
    }
}
