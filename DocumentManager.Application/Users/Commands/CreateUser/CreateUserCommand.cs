using DocumentManager.Application.Abstractions.Messaging;

namespace DocumentManager.Application.Users.Commands.CreateUser;

public sealed record CreateUserCommand(string Email, int OrganizationId) : ICommand<int>;
