using Dapper;
using DocumentManager.Application.Abstractions.Messaging;

using System.Data;

namespace DocumentManager.Application.Documents.Queries.GetAllDocuments;

internal sealed class GetAllDocumentsQueryHandler : IQueryHandler<GetAllDocumentsQuery, IEnumerable<DocumentResponse>>
{
    private readonly IDbConnection _dbConnection;
    public GetAllDocumentsQueryHandler(IDbConnection dbConnection) => _dbConnection = dbConnection;

    public async Task<IEnumerable<DocumentResponse>> Handle(GetAllDocumentsQuery request, CancellationToken cancellationToken)
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
            JOIN Users u ON u.OrganizationId = d.OrganizationId
            WHERE u.Id = @UserId OR d.OwnerId = @UserId";

        var documents = await _dbConnection.QueryAsync<DocumentResponse>(sql, new { request.UserId });

        return documents;
    }
}
