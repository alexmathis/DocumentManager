using DocumentManager.Domain.Entities;
using MediatR;


namespace DocumentManager.Application.Documents.Queries.GetAllDocuments;

public class GetAllDocumentsQuery : IRequest<IEnumerable<Document>>
{
}