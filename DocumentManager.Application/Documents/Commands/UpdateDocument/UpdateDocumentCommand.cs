using DocumentManager.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;

public sealed record UpdateDocumentCommand(int Id, string Name, IFormFile? File, int EditingUserId) : ICommand<Unit>;
