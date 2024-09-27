using DocumentManager.Application.Abstractions.Messaging;
using DocumentManager.Domain.Abstractions;
using DocumentManager.Domain.Exceptions;
using DocumentManager.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManager.Application.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand, Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(command.Id);
        if (user == null)
        {
            throw new UserNotFoundException(command.Id);
        }

        var requestingUser = await _userRepository.GetByIdAsync(command.RequestingUserId);
        if (requestingUser == null)
        {
            throw new UserNotFoundException(command.RequestingUserId);
        }

        if (requestingUser.Id != user.Id && requestingUser.OrganizationId != user.OrganizationId)
        {
            throw new UnauthorizedAccessException("You do not have permission to delete this user.");
        }

        _userRepository.Delete(user);

        // Save changes using Unit of Work with cancellation token
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
