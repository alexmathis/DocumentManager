using DocumentManager.Application.Abstractions.Messaging;


namespace DocumentManager.Application.Users.Commands.DeleteUser;

public sealed record DeleteUserCommand(int Id, int RequestingUserId) : ICommand<Unit>;  
