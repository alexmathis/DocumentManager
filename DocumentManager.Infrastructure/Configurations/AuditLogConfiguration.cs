using DocumentManager.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

internal sealed class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable("AuditLogs");

        builder.HasKey(al => al.Id);

        builder.Property(al => al.DocumentId)
               .IsRequired();

        builder.Property(al => al.UserId)
               .IsRequired();

        builder.Property(al => al.Action)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(al => al.Timestamp)
               .IsRequired();

        builder.HasIndex(al => al.Timestamp);

        builder.Ignore(al => al.Document);
        builder.Ignore(al => al.User);
    }
}
