using DocumentManager.Application.Abstractions.Messaging;
using DocumentManager.Application.Users.Queries.GetOrganizationById;

namespace DocumentManager.Application.Organizations.Queries.GetAllOrganizations;

public sealed record GetAllOrganizationsQuery() : IQuery<IEnumerable<OrganizationResponse>>;

