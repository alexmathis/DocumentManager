using DocumentManager.Application.Abstractions.Messaging;
using DocumentManager.Domain.Abstractions;
using DocumentManager.Domain.Entities;
using DocumentManager.Domain.Interfaces;


namespace DocumentManager.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, int>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(
            email: request.Email,
            organizationId: request.OrganizationId
        );

        _userRepository.Insert(user);

        await _unitOfWork.SaveChangesAsync();

        return user.Id;
    }
}