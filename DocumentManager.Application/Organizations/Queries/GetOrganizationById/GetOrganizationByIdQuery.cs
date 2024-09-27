using DocumentManager.Application.Abstractions.Messaging;
using DocumentManager.Application.Users.Queries.GetOrganizationById;

namespace DocumentManager.Application.Organizations.Queries.GetOrganizationById;
public sealed record GetOrganizationByIdQuery(int Id) : IQuery<OrganizationResponse>;


