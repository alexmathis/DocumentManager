namespace DocumentManager.Application.Users.Commands.CreateUser;

public sealed record CreateUserRequest(string Email, int OrganizationId);