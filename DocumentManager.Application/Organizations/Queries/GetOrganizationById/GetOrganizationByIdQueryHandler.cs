using DocumentManager.Domain.Entities;
using DocumentManager.Domain.Interfaces;
using MediatR;


namespace DocumentManager.Application.Organizations.Queries.GetOrganizationById;

public class GetOrganizationByIdQueryHandler : IRequestHandler<GetOrganizationByIdQuery, Organization?>
{
    private readonly IOrganizationRepository _organizationRepository;

    public GetOrganizationByIdQueryHandler(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    public async Task<Organization?> Handle(GetOrganizationByIdQuery request, CancellationToken cancellationToken)
    {
        // Retrieve the organization by Id
        return await _organizationRepository.GetByIdAsync(request.Id);
    }
}
