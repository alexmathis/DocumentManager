using DocumentManager.Domain.Entities;
using MediatR;


namespace DocumentManager.Application.Organizations.Queries.GetOrganizationById;
public class GetOrganizationByIdQuery : IRequest<Organization?>
{
    public int Id { get; set; }
}

