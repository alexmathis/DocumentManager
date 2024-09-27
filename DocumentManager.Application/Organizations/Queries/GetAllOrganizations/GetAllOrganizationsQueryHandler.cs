using Dapper;
using DocumentManager.Application.Abstractions.Messaging;
using DocumentManager.Application.Organizations.Queries.GetOrganizationById;
using DocumentManager.Application.Users.Queries.GetOrganizationById;
using DocumentManager.Domain.Entities;
using DocumentManager.Domain.Exceptions;
using DocumentManager.Domain.Interfaces;
using MediatR;
using System.Data;

namespace DocumentManager.Application.Organizations.Queries.GetAllOrganizations;

internal sealed class GetAllOrganizationsQueryHandler : IQueryHandler<GetAllOrganizationsQuery, IEnumerable<OrganizationResponse>>
{
    private readonly IDbConnection _dbConnection;
    public GetAllOrganizationsQueryHandler(IDbConnection dbConnection) => _dbConnection = dbConnection;


    public async Task<IEnumerable<OrganizationResponse>> Handle(GetAllOrganizationsQuery request, CancellationToken cancellationToken)
    {
        var sql = $"SELECT {nameof(OrganizationResponse.Id)}, {nameof(OrganizationResponse.Name)}  FROM Organizations";

        var organizations = await _dbConnection.QueryAsync<OrganizationResponse>(sql);

        return organizations;
    }
}
