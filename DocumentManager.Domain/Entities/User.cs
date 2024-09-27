namespace DocumentManager.Domain.Entities;
public sealed class User
{
    // Private constructor for factory use and EF Core
    private User(string email, int organizationId)
    {
        Email = email ?? throw new ArgumentNullException(nameof(email));
        OrganizationId = organizationId;
        Documents = new List<Document>();  
    }

    private User() { }

    public static User Create(string email, int organizationId)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be null or empty.", nameof(email));

        if (organizationId <= 0)
            throw new ArgumentException("OrganizationId must be a positive integer.", nameof(organizationId));

        return new User(email, organizationId);
    }

    public int Id { get; private set; } 
    public string Email { get; private set; }
    public int OrganizationId { get; private set; }
    public Organization Organization { get; private set; } 
    public ICollection<Document> Documents { get; private set; }




    public void UpdateEmail(string newEmail)
    {
        if (string.IsNullOrWhiteSpace(newEmail))
            throw new ArgumentException("Email cannot be null or empty.", nameof(newEmail));

        Email = newEmail;
    }


    public void UpdateOrganizationId(int newOrganizationId)
    {
        if (newOrganizationId <= 0)
            throw new ArgumentException("OrganizationId must be a positive integer.", nameof(newOrganizationId));

        OrganizationId = newOrganizationId;
    }
}
