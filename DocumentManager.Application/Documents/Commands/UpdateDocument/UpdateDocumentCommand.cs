using MediatR;

namespace DocumentManager.Application.Documents.Commands.UpdateDocument;

public class UpdateDocumentCommand : IRequest<Unit>  // Unit is equivalent to void
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Size { get; set; }
    public string StoragePath { get; set; }
}
