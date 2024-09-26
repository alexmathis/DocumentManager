using MediatR;

namespace DocumentManager.Application.Users.Commands.UpdateUser;


public class UpdateUserCommand : IRequest<Unit>  // Returns Unit (void equivalent)
{
    public int Id { get; set; }
    public string Email { get; set; }
    public int OrganizationId { get; set; }
}