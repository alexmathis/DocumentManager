using DocumentManager.Domain.Primatives;

namespace DocumentManager.Domain.Entities;

public sealed class Organization: Entity
{
    // Private constructor for EF Core and factory method usage
    private Organization(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Users = new List<User>();
        Documents = new List<Document>();
    }

    private Organization() { }


    public static Organization Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Organization name cannot be null or empty.", nameof(name));

        return new Organization(name);
    }


    public int Id { get; private set; }
    public string Name { get; private set; }
    public ICollection<User> Users { get; private set; }
    public ICollection<Document> Documents { get; private set; }


    public void UpdateName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Organization name cannot be null or empty.", nameof(newName));

        Name = newName;
    }


    public void AddUser(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        if (!Users.Contains(user))
        {
            Users.Add(user);
        }
    }

    public void RemoveUser(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        Users.Remove(user);
    }


    public void AddDocument(Document document)
    {
        if (document == null) throw new ArgumentNullException(nameof(document));

        if (!Documents.Contains(document))
        {
            Documents.Add(document);
        }
    }

    public void RemoveDocument(Document document)
    {
        if (document == null) throw new ArgumentNullException(nameof(document));

        Documents.Remove(document);
    }



}

