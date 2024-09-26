using DocumentManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DocumentManager.Infrastructure.Configurations;

internal sealed class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.ToTable("Documents");

        builder.HasKey(d => d.Id);


        builder.Property(d => d.Name)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(d => d.Size)
               .IsRequired();

        builder.Property(d => d.StoragePath)
               .IsRequired()
               .HasMaxLength(500);

      
        builder.HasOne(d => d.Owner)
               .WithMany(u => u.Documents)
               .HasForeignKey(d => d.OwnerId)
               .OnDelete(DeleteBehavior.Restrict); 

        builder.HasOne(d => d.Organization)
               .WithMany(o => o.Documents)
               .HasForeignKey(d => d.OrganizationId)
               .OnDelete(DeleteBehavior.Cascade); 


    }
}
