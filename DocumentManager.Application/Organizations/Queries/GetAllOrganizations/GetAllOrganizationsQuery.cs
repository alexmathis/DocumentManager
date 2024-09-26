using DocumentManager.Domain.Entities;
using MediatR;


namespace DocumentManager.Application.Organizations.Queries.GetAllOrganizations;
public class GetAllOrganizationsQuery : IRequest<IEnumerable<Organization>>
{
}

