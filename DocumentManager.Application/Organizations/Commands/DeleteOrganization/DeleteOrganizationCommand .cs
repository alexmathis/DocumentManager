using DocumentManager.Application.Abstractions.Messaging;

namespace DocumentManager.Application.Organizations.Commands.DeleteOrganization;
public sealed record  DeleteOrganizationCommand(int Id, int DeletingUserId) : ICommand<Unit>;  

