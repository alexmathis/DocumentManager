using DocumentManager.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace DocumentManager.Infrastructure.Configurations;

public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable("AuditLogs");

        builder.HasKey(al => al.Id);
       
        builder.HasOne(al => al.Document)
               .WithMany()
               .HasForeignKey(al => al.DocumentId)
               .OnDelete(DeleteBehavior.Restrict);  

 
        builder.HasOne(al => al.User)
               .WithMany()
               .HasForeignKey(al => al.UserId)
               .OnDelete(DeleteBehavior.Restrict); 
       
        builder.Property(al => al.Action)
               .IsRequired()
               .HasMaxLength(50); 
    
        builder.Property(al => al.Timestamp)
               .IsRequired();

        builder.HasIndex(al => al.Timestamp);
    }
}