namespace DocumentManager.Application.Users.Commands.UpdateUser;

public sealed record UpdateUserRequest(string Email, int OrganizationId);