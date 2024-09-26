using DocumentManager.Domain.Entities;
using DocumentManager.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DocumentManager.Infrastructure.Persistence.Repositories;

    public sealed class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<User?> GetByIdAsync(int id)
        {

            return await _dbContext.Set<User>()
                                   .Include(u => u.Organization)  // Include related Organization
                                   .Include(u => u.Documents)    // Include related Documents
                                   .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbContext.Set<User>()
                                   .Include(u => u.Organization)  // Include related Organization
                                   .Include(u => u.Documents)    // Include related Documents
                                   .ToListAsync();
        }

        public void Insert(User user) => _dbContext.Set<User>().Add(user);

        public void Update(User user) => _dbContext.Set<User>().Update(user);

        public void Delete(User user) => _dbContext.Set<User>().Remove(user);

        public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }

