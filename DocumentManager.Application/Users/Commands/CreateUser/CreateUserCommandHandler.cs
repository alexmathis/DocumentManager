using DocumentManager.Domain.Entities;
using DocumentManager.Domain.Interfaces;
using MediatR;


namespace DocumentManager.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Use the factory method to create a new User
        var user = User.Create(
            email: request.Email,
            organizationId: request.OrganizationId
        );

        // Insert the user into the repository
        _userRepository.Insert(user);

        // Save changes to the database
        await _userRepository.SaveChangesAsync();

        // Return the newly created user's ID
        return user.Id;
    }
}