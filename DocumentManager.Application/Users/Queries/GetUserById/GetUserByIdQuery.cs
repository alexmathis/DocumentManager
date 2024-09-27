using DocumentManager.Application.Abstractions.Messaging;
using DocumentManager.Domain.Entities;

namespace DocumentManager.Application.Users.Queries.GetUserById;

public sealed record GetUserByIdQuery(int Id, int RequestingUserId) : IQuery<UserResponse>;
