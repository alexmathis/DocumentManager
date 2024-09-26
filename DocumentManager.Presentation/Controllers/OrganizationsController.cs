using Microsoft.AspNetCore.Mvc;
using MediatR;
using DocumentManager.Application.Organizations.Queries.GetOrganizationById;
using DocumentManager.Application.Organizations.Queries.GetAllOrganizations;
using DocumentManager.Application.Organizations.Commands.CreateOrganization;
using DocumentManager.Application.Organizations.Commands.UpdateOrganization;
using DocumentManager.Application.Organizations.Commands.DeleteOrganization;
namespace Document_Manager.Presentation.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrganizationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/organization/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var organization = await _mediator.Send(new GetOrganizationByIdQuery { Id = id });

            if (organization == null)
            {
                return NotFound();
            }

            return Ok(organization);
        }

        // GET: api/organization
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var organizations = await _mediator.Send(new GetAllOrganizationsQuery());
            return Ok(organizations);
        }

        // POST: api/organization
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrganizationCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var organizationId = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = organizationId }, organizationId);
        }

        // PUT: api/organization/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateOrganizationCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("The organization ID in the URL doesn't match the ID in the body.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _mediator.Send(command);
            return NoContent();
        }

        // DELETE: api/organization/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteOrganizationCommand { Id = id };

            await _mediator.Send(command);

            return NoContent();
        }
    }

