using MediatR;

namespace DocumentManager.Application.Organizations.Commands.CreateOrganization;

public class CreateOrganizationCommand : IRequest<int>  // Returns the created organization's Id
{
    public string Name { get; set; }
}
