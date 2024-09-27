using DocumentManager.Application.Abstractions.Messaging;

namespace DocumentManager.Application.Organizations.Commands.UpdateOrganization;
public sealed record UpdateOrganizationCommand(int Id, string Name, int UpdatingUserId) : ICommand<Unit>;


