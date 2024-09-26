using DocumentManager.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;


namespace DocumentManager.Infrastructure;

    public class ApplicationDbContext:  DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

