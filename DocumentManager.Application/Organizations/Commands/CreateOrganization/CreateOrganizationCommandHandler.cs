using DocumentManager.Application.Abstractions.Messaging;
using DocumentManager.Domain.Abstractions;
using DocumentManager.Domain.Entities;
using DocumentManager.Domain.Interfaces;


namespace DocumentManager.Application.Organizations.Commands.CreateOrganization;
public class CreateOrganizationCommandHandler : ICommandHandler<CreateOrganizationCommand, int>
{
    private readonly IOrganizationRepository _organizationRepository;

    private readonly IUnitOfWork _unitOfWork;

    public CreateOrganizationCommandHandler(IOrganizationRepository organizationRepository, IUnitOfWork unitOfWork)
    {
        _organizationRepository = organizationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
    {
        var organization = Organization.Create(request.Name);

        _organizationRepository.Insert(organization);

        await _unitOfWork.SaveChangesAsync();

        return organization.Id;
    }
}

