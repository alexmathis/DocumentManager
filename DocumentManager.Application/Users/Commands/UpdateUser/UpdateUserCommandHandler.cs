using DocumentManager.Application.Abstractions.Messaging;
using DocumentManager.Domain.Abstractions;
using DocumentManager.Domain.Exceptions;
using DocumentManager.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManager.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);
        if (user == null)
        {
            throw new UserNotFoundException(request.Id);
        }

        var requestingUser = await _userRepository.GetByIdAsync(request.RequestingUserId);
        if (requestingUser == null)
        {
            throw new UserNotFoundException(request.RequestingUserId);
        }

        if (requestingUser.Id != user.Id && requestingUser.OrganizationId != user.OrganizationId)
        {
            throw new UnauthorizedAccessException("You do not have permission to update this user.");
        }

        user.UpdateEmail(request.Email);
        user.UpdateOrganizationId(request.OrganizationId);

        _userRepository.Update(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
