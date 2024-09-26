using DocumentManager.Domain.Interfaces;
using MediatR;

namespace DocumentManager.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the user by Id
        var user = await _userRepository.GetByIdAsync(request.Id);

        if (user == null)
        {
            throw new KeyNotFoundException($"User with Id {request.Id} not found.");
        }

        // Update the user's email and organization ID using the new methods
        user.UpdateEmail(request.Email);
        user.UpdateOrganizationId(request.OrganizationId);

        // Update the user in the repository
        _userRepository.Update(user);

        // Save changes to the database
        await _userRepository.SaveChangesAsync();

        return Unit.Value; // Return Unit to indicate success
    }
}