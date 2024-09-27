using Microsoft.AspNetCore.Mvc;
using DocumentManager.Application.Organizations.Queries.GetOrganizationById;
using DocumentManager.Application.Organizations.Queries.GetAllOrganizations;
using DocumentManager.Application.Organizations.Commands.CreateOrganization;
using DocumentManager.Application.Organizations.Commands.UpdateOrganization;
using DocumentManager.Application.Organizations.Commands.DeleteOrganization;
using DocumentManager.Presentation.Controllers;
using DocumentManager.Application.Users.Queries.GetUserById;
using Microsoft.AspNetCore.Http;
using Mapster;
using DocumentManager.Application.Users.Queries.GetOrganizationById;



namespace Document_Manager.Presentation.Controllers;

public class OrganizationsController : ApiController
{

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id, [FromHeader(Name = "X-UserId")] int userId)
    {
        if (userId <= 0)
        {
            return BadRequest("Authorized user required.");
        }

        var organization = await Sender.Send(new GetOrganizationByIdQuery(id));

        return Ok(organization);
    }


    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OrganizationResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromHeader(Name = "X-UserId")] int userId)
    {
        if (userId <= 0)
        {
            return BadRequest("Authorized user required.");
        }

        var organizations = await Sender.Send(new GetAllOrganizationsQuery());
        return Ok(organizations);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrganizationRequest request, [FromHeader(Name = "X-UserId")] int userId)
    {
    
        var command = new CreateOrganizationCommand(request.Name, userId);
        var organizationId = await Sender.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = organizationId }, organizationId);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateOrganizationRequest request, [FromHeader(Name = "X-UserId")] int userId)
    {

        var command = new UpdateOrganizationCommand(id, request.Name, userId);

        await Sender.Send(command);

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, [FromHeader(Name = "X-UserId")] int userId)
    {
        var command = new DeleteOrganizationCommand(id, userId);

        await Sender.Send(command);

        return NoContent();
    }
}

