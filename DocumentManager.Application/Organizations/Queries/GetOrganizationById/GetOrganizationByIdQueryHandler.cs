using Dapper;
using DocumentManager.Application.Abstractions.Messaging;
using DocumentManager.Application.Users.Queries.GetOrganizationById;
using DocumentManager.Domain.Exceptions;
using System.Data;



namespace DocumentManager.Application.Organizations.Queries.GetOrganizationById;


internal sealed class GetOrganizationByIdQueryHandler : IQueryHandler<GetOrganizationByIdQuery, OrganizationResponse>
{
    private readonly IDbConnection _dbConnection;
    public GetOrganizationByIdQueryHandler(IDbConnection dbConnection) => _dbConnection = dbConnection;


    public async Task<OrganizationResponse> Handle(GetOrganizationByIdQuery request, CancellationToken cancellationToken)
    {
        var sql = $"SELECT {nameof(OrganizationResponse.Id)}, {nameof(OrganizationResponse.Name)} FROM Organizations WHERE Id = @Id";

        var organization = await _dbConnection.QueryFirstOrDefaultAsync<OrganizationResponse>(sql, new { request.Id });

        if (organization is null)
        {
            throw new OrganizationNotFoundException(request.Id);
        }

        return organization;
    }
}