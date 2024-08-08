using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuokkaDev.Templates.Domain.Samples.HelloWorldMessages;

namespace QuokkaDev.Templates.Persistence.Ef.Samples.HelloWorldMessages
{
    public class HelloWorldMessageConfiguration : IEntityTypeConfiguration<HelloWorldMessage>
    {
        public void Configure(EntityTypeBuilder<HelloWorldMessage> builder)
        {
            builder.ToTable(nameof(HelloWorldMessage));
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Message).HasMaxLength(500).IsRequired();

            builder.Ignore(x => x.DomainEvents);
        }
    }
}
