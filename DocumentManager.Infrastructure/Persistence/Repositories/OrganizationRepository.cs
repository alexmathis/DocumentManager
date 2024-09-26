using DocumentManager.Domain.Entities;
using DocumentManager.Domain.Interfaces;
using DocumentManager.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocumentManager.Infrastructure.Persistence.Repositories;

    public sealed class OrganizationRepository : IOrganizationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrganizationRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<Organization?> GetByIdAsync(int id)
        {

        return await _dbContext.Set<Organization>()
                                   .Include(o => o.Users)  // Optional: Include related Users if needed
                                   .Include(o => o.Documents) // Optional: Include related Documents if needed
                                   .FirstOrDefaultAsync(o => o.Id == id);

        }

        public async Task<IEnumerable<Organization>> GetAllAsync()
        {
            return await _dbContext.Set<Organization>().ToListAsync();
        }

        public void Insert(Organization organization) => _dbContext.Set<Organization>().Add(organization);

        public void Update(Organization organization) => _dbContext.Set<Organization>().Update(organization);

        public void Delete(Organization organization) => _dbContext.Set<Organization>().Remove(organization);

        public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }

