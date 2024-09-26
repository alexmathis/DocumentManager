namespace DocumentManager.Domain.Entities;

public class AuditLog
{
    public int Id { get; private set; }        

    public int DocumentId { get; private set; } 
    public Document Document { get; private set; } 

    public int UserId { get; private set; }  
    public User User { get; private set; }     

    public string Action { get; private set; } 
    public DateTime Timestamp { get; private set; }

    // Constructor for EF Core
    private AuditLog() { }

    public static AuditLog Create(int documentId, int userId, string action)
    {
        if (string.IsNullOrEmpty(action))
        {
            throw new ArgumentException("Action cannot be null or empty.", nameof(action));
        }

        return new AuditLog
        {
            DocumentId = documentId,
            UserId = userId,
            Action = action,
            Timestamp = DateTime.UtcNow
        };
    }
}
