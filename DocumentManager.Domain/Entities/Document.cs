using DocumentManager.Domain.Primatives;

namespace DocumentManager.Domain.Entities;
public sealed class Document
{
    // Private constructor for factory use and EF Core
    private Document(string name, int size, string storagePath, int ownerId, int organizationId)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Size = size > 0 ? size : throw new ArgumentException("Document size must be greater than 0.", nameof(size));
        StoragePath = !string.IsNullOrWhiteSpace(storagePath)
            ? storagePath
            : throw new ArgumentException("Storage path cannot be null or empty.", nameof(storagePath));
        OwnerId = ownerId;
        OrganizationId = organizationId;
    }
    private Document() { }

    public static Document Create(string name, int size, string storagePath, int ownerId, int organizationId)
    {
        
        return new Document(name, size, storagePath, ownerId, organizationId);
    }


    public int Id { get; private set; } 
    public string Name { get; private set; }
    public int Size { get; private set; }
    public string StoragePath { get; private set; }
    public int OwnerId { get; private set; }  
    public User Owner { get; private set; }   
    public int OrganizationId { get; private set; }
    public Organization Organization { get; private set; } 

   
    public void UpdateDocument(string newName, int newSize, string newStoragePath)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Document name cannot be null or empty.", nameof(newName));
        if (newSize <= 0)
            throw new ArgumentException("Document size must be greater than 0.", nameof(newSize));
        if (string.IsNullOrWhiteSpace(newStoragePath))
            throw new ArgumentException("Storage path cannot be null or empty.", nameof(newStoragePath));

        Name = newName;
        Size = newSize;
        StoragePath = newStoragePath;
    }
}
