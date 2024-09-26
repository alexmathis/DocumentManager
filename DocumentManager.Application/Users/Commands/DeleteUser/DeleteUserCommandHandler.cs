using DocumentManager.Domain.Interfaces;
using MediatR;


namespace DocumentManager.Application.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the user by Id
        var user = await _userRepository.GetByIdAsync(request.Id);

        if (user == null)
        {
            throw new KeyNotFoundException($"User with Id {request.Id} not found.");
        }

        // Delete the user
        _userRepository.Delete(user);

        // Save changes to the database
        await _userRepository.SaveChangesAsync();

        return Unit.Value; // Return Unit to indicate success
    }
}