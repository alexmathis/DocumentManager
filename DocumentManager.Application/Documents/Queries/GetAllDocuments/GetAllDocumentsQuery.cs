using DocumentManager.Application.Abstractions.Messaging;


namespace DocumentManager.Application.Documents.Queries.GetAllDocuments;

public sealed record GetAllDocumentsQuery(int UserId) : IQuery<IEnumerable<DocumentResponse>>;