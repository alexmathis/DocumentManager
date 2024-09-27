using Dapper;
using DocumentManager.Application.Abstractions.Messaging;
using DocumentManager.Application.Users.Queries.GetUserById;
using DocumentManager.Domain.Exceptions;
using System.Data;

namespace DocumentManager.Application.Users.Queries.GetUserById;

internal sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    private readonly IDbConnection _dbConnection;
    public GetUserByIdQueryHandler(IDbConnection dbConnection) => _dbConnection = dbConnection;

    public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var sql = $@"
            SELECT u.{nameof(UserResponse.Id)}, 
                   u.{nameof(UserResponse.Email)}, 
                   u.{nameof(UserResponse.OrganizationId)}  
            FROM Users u
            JOIN Users requestingUser ON requestingUser.Id = @RequestingUserId
            WHERE u.Id = @Id AND u.OrganizationId = requestingUser.OrganizationId";

        var user = await _dbConnection.QueryFirstOrDefaultAsync<UserResponse>(sql, new { request.Id, request.RequstingUserId });

        if (user is null)
        {
            throw new UserNotFoundException(request.Id);
        }

        return user;
    }
}
