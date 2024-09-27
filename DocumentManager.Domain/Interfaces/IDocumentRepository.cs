using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentManager.Domain.Entities;

namespace DocumentManager.Domain.Interfaces;
    public interface IDocumentRepository
    {
        Task<Document?> GetByIdAsync(int id);
        Task<IEnumerable<Document>> GetAllAsync();  
        Task<int> Insert(Document document);
        void Update(Document document);  
        void Delete(Document document);  
        Task SaveChangesAsync();  
    }

