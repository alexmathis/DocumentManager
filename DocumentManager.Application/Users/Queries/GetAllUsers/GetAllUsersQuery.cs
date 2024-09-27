using DocumentManager.Application.Abstractions.Messaging;
using DocumentManager.Application.Users.Queries.GetUserById;

namespace DocumentManager.Application.Users.Queries.GetAllUsers;

public sealed record GetAllUsersQuery(int RequstingUserId) : IQuery<IEnumerable<UserResponse>>;
