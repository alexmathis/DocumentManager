using Microsoft.AspNetCore.Http;

namespace DocumentManager.Application.Documents.Commands.CreateDocument;

public sealed record CreateDocumentRequest(string Name, IFormFile File, int UserId, int OrganizationId);
