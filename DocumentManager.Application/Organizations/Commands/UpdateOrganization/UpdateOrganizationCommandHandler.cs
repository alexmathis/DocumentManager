using DocumentManager.Application.Abstractions.Messaging;
using DocumentManager.Domain.Abstractions;
using DocumentManager.Domain.Exceptions;
using DocumentManager.Domain.Interfaces;


namespace DocumentManager.Application.Organizations.Commands.UpdateOrganization;

public class UpdateOrganizationCommandHandler : ICommandHandler<UpdateOrganizationCommand, Unit>
{
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateOrganizationCommandHandler(IOrganizationRepository organizationRepository, IUnitOfWork unitOfWork)
    {
        _organizationRepository = organizationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateOrganizationCommand request, CancellationToken cancellationToken)
    {
    
        var organization = await _organizationRepository.GetByIdAsync(request.Id);

        if (organization == null)
        {
            throw new OrganizationNotFoundException(request.Id);
        }
  
        organization.UpdateName(request.Name);

       
        _organizationRepository.Update(organization);

       
        await _unitOfWork.SaveChangesAsync();

        return Unit.Value; 
    }
}