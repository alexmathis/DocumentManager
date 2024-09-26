using MediatR;


namespace DocumentManager.Application.Documents.Commands.CreateDocument;

public class CreateDocumentCommand : IRequest<int>  // Returns the created Document's Id
{
    public string Name { get; set; }
    public int Size { get; set; }
    public string StoragePath { get; set; }
    public int UserId { get; set; }
    public int OrganizationId { get; set; }
}
