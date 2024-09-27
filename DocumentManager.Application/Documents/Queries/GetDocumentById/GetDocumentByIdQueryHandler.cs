using Dapper;
using DocumentManager.Application.Abstractions.Messaging;
using DocumentManager.Domain.Exceptions;
using System.Data;

namespace DocumentManager.Application.Documents.Queries.GetDocumentById;

internal sealed class GetDocumentByIdQueryHandler : IQueryHandler<GetDocumentByIdQuery, DocumentResponse>
{
    private readonly IDbConnection _dbConnection;

    public GetDocumentByIdQueryHandler(IDbConnection dbConnection) => _dbConnection = dbConnection;

    public async Task<DocumentResponse> Handle(GetDocumentByIdQuery request, CancellationToken cancellationToken)
    {
        var sql = $@"
            SELECT 
                d.{nameof(DocumentResponse.Id)}, 
                d.{nameof(DocumentResponse.Name)}, 
                d.{nameof(DocumentResponse.Size)}, 
                d.{nameof(DocumentResponse.StoragePath)}, 
                d.{nameof(DocumentResponse.OwnerId)}, 
                d.{nameof(DocumentResponse.OrganizationId)}
            FROM Documents d
            JOIN Users u ON u.Id = @UserId
            WHERE d.Id = @Id
            AND (d.OwnerId = @UserId OR d.OrganizationId = u.OrganizationId)";

        var document = await _dbConnection.QueryFirstOrDefaultAsync<DocumentResponse>(sql, new { request.Id, request.UserId });

        if (document is null)
        {
            throw new DocumentNotFoundException(request.Id);
        }

        return document;
    }
}
