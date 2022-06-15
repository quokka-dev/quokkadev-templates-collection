namespace StorePortal.Infrastructure.SchemaDefinitions
{
    public class $safeitemname$Configuration : IEntityTypeConfiguration<$safeitemname$>
    {
        public void Configure(EntityTypeBuilder<$safeitemname$> builder)
    {
        builder.ToTable("MyEntityTableName");
    }
}
}
