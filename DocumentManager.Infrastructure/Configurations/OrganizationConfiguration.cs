using DocumentManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DocumentManager.Infrastructure.Configurations;

internal sealed class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.ToTable("Organizations");

        // Primary key
        builder.HasKey(o => o.Id);

        // Property configurations
        builder.Property(o => o.Name)
               .IsRequired()
               .HasMaxLength(100);

        // Relationships
        builder.HasMany(o => o.Users)
               .WithOne(u => u.Organization)
               .HasForeignKey(u => u.OrganizationId)
               .OnDelete(DeleteBehavior.Cascade); // If Organization is deleted, delete associated Users

        builder.HasMany(o => o.Documents)
               .WithOne(d => d.Organization)
               .HasForeignKey(d => d.OrganizationId)
               .OnDelete(DeleteBehavior.Cascade); // If Organization is deleted, delete associated Documents
    }
}

