using MediatR;


namespace DocumentManager.Application.Organizations.Commands.UpdateOrganization;
public class UpdateOrganizationCommand : IRequest<Unit>  // Unit is equivalent to void
{
    public int Id { get; set; }
    public string Name { get; set; }
}

