using DocumentManager.Application.Abstractions.Messaging;
using DocumentManager.Application.Users.Queries.GetUserById;

namespace DocumentManager.Application.Users.Queries.GetAllUsers;

public sealed record GetAllUsersQuery(int RequestingUserId) : IQuery<IEnumerable<UserResponse>>;
