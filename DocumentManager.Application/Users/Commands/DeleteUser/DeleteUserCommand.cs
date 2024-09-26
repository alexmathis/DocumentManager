using MediatR;


namespace DocumentManager.Application.Users.Commands.DeleteUser;

public class DeleteUserCommand : IRequest<Unit>  // Returns Unit for delete
{
    public int Id { get; set; }
}