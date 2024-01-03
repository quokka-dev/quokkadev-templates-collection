using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QuokkaDev.Templates.Persistence.Ef.MyAggregateRoot
{
    internal class MyAggregateRootConfig : IEntityTypeConfiguration<Domain.MyAggregateRoot.MyAggregateRoot>
    {
        public void Configure(EntityTypeBuilder<Domain.MyAggregateRoot.MyAggregateRoot> builder)
        {
            builder.ToTable(nameof(Domain.MyAggregateRoot.MyAggregateRoot));
            builder.HasKey(ie => ie.Id);

            builder.Ignore(rli => rli.DomainEvents);
        }
    }
}
