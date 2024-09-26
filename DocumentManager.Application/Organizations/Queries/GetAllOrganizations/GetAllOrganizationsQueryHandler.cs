using DocumentManager.Domain.Entities;
using DocumentManager.Domain.Interfaces;
using MediatR;

namespace DocumentManager.Application.Organizations.Queries.GetAllOrganizations;

public class GetAllOrganizationsQueryHandler : IRequestHandler<GetAllOrganizationsQuery, IEnumerable<Organization>>
{
    private readonly IOrganizationRepository _organizationRepository;

    public GetAllOrganizationsQueryHandler(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    public async Task<IEnumerable<Organization>> Handle(GetAllOrganizationsQuery request, CancellationToken cancellationToken)
    {
        // Retrieve all organizations
        return await _organizationRepository.GetAllAsync();
    }
}
