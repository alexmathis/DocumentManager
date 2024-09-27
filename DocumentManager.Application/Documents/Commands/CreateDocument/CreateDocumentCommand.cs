using DocumentManager.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;

namespace DocumentManager.Application.Documents.Commands.CreateDocument;

public sealed record CreateDocumentCommand(string Name, IFormFile File, int UserId, int OrganizationId) : ICommand<int>;


