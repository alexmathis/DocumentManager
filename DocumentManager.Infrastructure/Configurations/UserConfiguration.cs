using DocumentManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocumentManager.Infrastructure.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        // Primary key
        builder.HasKey(u => u.Id);

        // Property configurations
        builder.Property(u => u.Email)
               .IsRequired()
               .HasMaxLength(100);

        // Relationships
        builder.HasOne(u => u.Organization)
               .WithMany(o => o.Users)
               .HasForeignKey(u => u.OrganizationId)
               .OnDelete(DeleteBehavior.Cascade); // If User is deleted, handle organization appropriately

        builder.HasMany(u => u.Documents)
               .WithOne(d => d.Owner)
               .HasForeignKey(d => d.OwnerId)
               .OnDelete(DeleteBehavior.Restrict); // If User is deleted, documents stay with the organization
    }
}

