using DocumentManager.Application.Abstractions.Messaging;

namespace DocumentManager.Application.Organizations.Commands.CreateAuditLog;

public sealed record CreateAuditLogCommand(int DocumentId, int UserId, string Action) : ICommand<int>; 
