using DocumentManager.Domain.Entities;
using MediatR;


namespace DocumentManager.Application.Documents.Queries.GetDocumentById;
public class GetDocumentByIdQuery : IRequest<Document?>
{
    public int Id { get; set; }
}
