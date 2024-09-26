using DocumentManager.Domain.Entities;
using DocumentManager.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentManager.Application.Organizations.Commands.CreateOrganization;
public class CreateOrganizationCommandHandler : IRequestHandler<CreateOrganizationCommand, int>
{
    private readonly IOrganizationRepository _organizationRepository;

    public CreateOrganizationCommandHandler(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    public async Task<int> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
    {
        var organization = Organization.Create(request.Name);


        _organizationRepository.Insert(organization);
        await _organizationRepository.SaveChangesAsync();

        return organization.Id;
    }
}

