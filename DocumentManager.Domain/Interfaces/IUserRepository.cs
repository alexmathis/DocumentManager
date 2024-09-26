using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentManager.Domain.Entities;

namespace DocumentManager.Domain.Interfaces;
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);  // Retrieves a User by Id, nullable if not found
        Task<IEnumerable<User>> GetAllAsync();  // Retrieves all Users
        void Insert(User user);  // Adds a new User
        void Update(User user);  // Updates an existing User
        void Delete(User user);  // Deletes a User
        Task SaveChangesAsync();  // Saves changes to the database
    }

