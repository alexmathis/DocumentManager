using DocumentManager.Domain.Entities;
using MediatR;


namespace DocumentManager.Application.Users.Queries.GetUserById;

public class GetUserByIdQuery : IRequest<User?>
{
    public int Id { get; set; }
}