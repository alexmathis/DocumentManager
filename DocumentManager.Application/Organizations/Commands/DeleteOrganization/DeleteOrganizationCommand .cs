using MediatR;


namespace DocumentManager.Application.Organizations.Commands.DeleteOrganization;
public class DeleteOrganizationCommand : IRequest<Unit>  // Returns Unit for delete
{
    public int Id { get; set; }
}
