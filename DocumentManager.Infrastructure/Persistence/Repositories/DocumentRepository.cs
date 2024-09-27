using DocumentManager.Domain.Entities;
using DocumentManager.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DocumentManager.Infrastructure.Persistence.Repositories;

public sealed class DocumentRepository : IDocumentRepository
{
    private readonly ApplicationDbContext _dbContext;

    public DocumentRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public async Task<Document?> GetByIdAsync(int id)
    {
        return await _dbContext.Set<Document>()
                               .Include(d => d.Owner)  // Include related User (Owner)
                               .Include(d => d.Organization) // Include related Organization
                               .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<IEnumerable<Document>> GetAllAsync()
    {
        return await _dbContext.Set<Document>()
                               .Include(d => d.Owner)         // Include related User (Owner)
                               .Include(d => d.Organization)  // Include related Organization
                               .ToListAsync();
    }

    public async Task<int> Insert(Document document)
    {
        _dbContext.Set<Document>().Add(document);
        await _dbContext.SaveChangesAsync();
        return document.Id;  // Return the document's Id after it has been saved
    }

    public void Update(Document document) => _dbContext.Set<Document>().Update(document);

    public void Delete(Document document) => _dbContext.Set<Document>().Remove(document);

    public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
}

