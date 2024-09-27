using DocumentManager.Application.Abstractions.Messaging;

namespace DocumentManager.Application.Organizations.Commands.CreateOrganization;

public sealed record CreateOrganizationCommand(string Name, int CreatingUserId) : ICommand<int>; 
