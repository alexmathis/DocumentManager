using DocumentManager.Application.Documents.Commands.CreateDocument;
using DocumentManager.Application.Documents.Commands.DeleteDocument;
using DocumentManager.Application.Documents.Commands.UpdateDocument;
using DocumentManager.Application.Documents.Queries.GetAllDocuments;
using DocumentManager.Application.Documents.Queries.GetDocumentById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Document_Manager.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DocumentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/documents/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var document = await _mediator.Send(new GetDocumentByIdQuery { Id = id });

        if (document == null)
        {
            return NotFound();
        }

        return Ok(document);
    }

    // GET: api/documents
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var documents = await _mediator.Send(new GetAllDocumentsQuery());
        return Ok(documents);
    }

    // POST: api/documents
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDocumentCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var documentId = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = documentId }, documentId);
    }

    // PUT: api/documents/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateDocumentCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("The document ID in the URL doesn't match the ID in the body.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _mediator.Send(command);
        return NoContent();
    }

    // DELETE: api/documents/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteDocumentCommand { Id = id };

        await _mediator.Send(command);

        return NoContent();
    }
}
