using DocumentManager.Application.Documents.Commands.CreateDocument;
using DocumentManager.Application.Documents.Commands.DeleteDocument;
using DocumentManager.Application.Documents.Commands.UpdateDocument;
using DocumentManager.Application.Documents.Queries.GetAllDocuments;
using DocumentManager.Application.Documents.Queries.GetDocumentById;
using Microsoft.AspNetCore.Mvc;
using DocumentManager.Presentation.Controllers;

namespace Document_Manager.Presentation.Controllers;

public class DocumentsController : ApiController
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, [FromHeader(Name = "X-UserId")] int userId)
    {
        var document = await Sender.Send(new GetDocumentByIdQuery(id, userId));
        return Ok(document);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromHeader(Name = "X-UserId")] int userId)
    {
        var documents = await Sender.Send(new GetAllDocumentsQuery(userId));
        return Ok(documents);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateDocumentRequest request, [FromHeader(Name = "X-UserId")] int userId)
    {
        if (userId != request.UserId)
        {
            return BadRequest("The document creator does not match the logged-in user.");
        }

        var command = new CreateDocumentCommand(request.Name, request.File, request.UserId, request.OrganizationId);
        var documentId = await Sender.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = documentId }, documentId);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromForm] UpdateDocumentRequest request, [FromHeader(Name = "X-UserId")] int userId)
    {
        var command = new UpdateDocumentCommand(id, request.Name, request.File, userId);
        await Sender.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, [FromHeader(Name = "X-UserId")] int userId)
    {
        var command = new DeleteDocumentCommand(id, userId);
        await Sender.Send(command);

        return NoContent();
    }
}
