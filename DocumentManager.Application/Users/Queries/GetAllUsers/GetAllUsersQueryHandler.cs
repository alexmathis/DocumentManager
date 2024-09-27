using Dapper;
using DocumentManager.Application.Abstractions.Messaging;
using DocumentManager.Application.Users.Queries.GetAllUsers;
using DocumentManager.Domain.Exceptions;
using DocumentManager.Domain.Entities;
using System.Data;
using DocumentManager.Application.Users.Queries.GetUserById;

namespace DocumentManager.Application.Users.Queries.GetAllUsers;

internal sealed class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, IEnumerable<UserResponse>>
{
    private readonly IDbConnection _dbConnection;

    public GetAllUsersQueryHandler(IDbConnection dbConnection) => _dbConnection = dbConnection;

    public async Task<IEnumerable<UserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        // First, fetch the organization of the requesting user
        var orgSql = @"SELECT OrganizationId FROM Users WHERE Id = @RequestingUserId";
        var organizationId = await _dbConnection.QueryFirstOrDefaultAsync<int?>(orgSql, new { request.RequstingUserId });

        if (organizationId == null)
        {
            throw new UnauthorizedAccessException();
        }

        var sql = @"
            SELECT Id, Email, OrganizationId 
            FROM Users 
            WHERE OrganizationId = @OrganizationId";

        var users = await _dbConnection.QueryAsync<UserResponse>(sql, new { OrganizationId = organizationId });

        return users;
    }
}
