using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentManager.Domain.Entities;

namespace DocumentManager.Domain.Interfaces;
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id); 
        Task<IEnumerable<User>> GetAllAsync(); 
        void Insert(User user);  
        void Update(User user);  
        void Delete(User user);  
        Task SaveChangesAsync();  
    }

