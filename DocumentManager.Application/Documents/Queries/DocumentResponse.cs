namespace DocumentManager.Application.Documents.Queries;
public sealed record DocumentResponse(int Id, string Name, int Size, string StoragePath, int OwnerId, int OrganizationId);


