using DocumentManager.Application.Abstractions.Messaging;
using DocumentManager.Domain.Abstractions;
using DocumentManager.Domain.Exceptions;
using DocumentManager.Domain.Interfaces;


namespace DocumentManager.Application.Organizations.Commands.DeleteOrganization;


public class DeleteOrganizationCommandHandler : ICommandHandler<DeleteOrganizationCommand, Unit>
{
    private readonly IOrganizationRepository _organizationRepository;

    private readonly IUnitOfWork _unitOfWork;

    public DeleteOrganizationCommandHandler(IOrganizationRepository organizationRepository, IUnitOfWork unitOfWork)
    {
        _organizationRepository = organizationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteOrganizationCommand request, CancellationToken cancellationToken)
    {
       
        var organization = await _organizationRepository.GetByIdAsync(request.Id);

        if (organization == null)
        {
            throw new OrganizationNotFoundException(request.Id);
        }

        _organizationRepository.Delete(organization);

        await _unitOfWork.SaveChangesAsync();

        return Unit.Value; 
    }
}