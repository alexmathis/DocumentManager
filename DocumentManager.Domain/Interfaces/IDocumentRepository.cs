using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentManager.Domain.Entities;

namespace DocumentManager.Domain.Interfaces;
    public interface IDocumentRepository
    {
        Task<Document?> GetByIdAsync(int id);  // Retrieves a Document by Id, nullable if not found
        Task<IEnumerable<Document>> GetAllAsync();  // Retrieves all Documents
        void Insert(Document document);  // Adds a new Document
        void Update(Document document);  // Updates an existing Document
        void Delete(Document document);  // Deletes a Document
        Task SaveChangesAsync();  // Saves changes to the database
    }

