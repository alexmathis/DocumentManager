using Microsoft.AspNetCore.Http;

namespace DocumentManager.Application.Documents.Commands.UpdateDocument;

public sealed record UpdateDocumentRequest(string Name, IFormFile? File); 
