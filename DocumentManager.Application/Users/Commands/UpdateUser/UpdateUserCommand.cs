using DocumentManager.Application.Abstractions.Messaging;

namespace DocumentManager.Application.Users.Commands.UpdateUser;


public sealed record UpdateUserCommand(int Id, string Email, int OrganizationId, int RequestingUserId) : ICommand<Unit>;
