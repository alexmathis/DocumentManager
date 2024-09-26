using DocumentManager.Domain.Entities;
using MediatR;

namespace DocumentManager.Application.Users.Queries.GetAllUsers;

public class GetAllUsersQuery : IRequest<IEnumerable<User>>
{
}
