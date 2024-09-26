using MediatR;

namespace DocumentManager.Application.Users.Commands.CreateUser;

public class CreateUserCommand : IRequest<int>  // Returns the created User's Id
{
    public string Email { get; set; }
    public int OrganizationId { get; set; }
}
