using DocumentManager.Application.Users.Commands.CreateUser;
using DocumentManager.Application.Users.Commands.DeleteUser;
using DocumentManager.Application.Users.Commands.UpdateUser;
using DocumentManager.Application.Users.Queries.GetAllUsers;
using DocumentManager.Application.Users.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Document_Manager.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/users/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery { Id = id });

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    // GET: api/users
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _mediator.Send(new GetAllUsersQuery());
        return Ok(users);
    }

    // POST: api/users
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userId = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = userId }, userId);
    }

    // PUT: api/users/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("The user ID in the URL doesn't match the ID in the body.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _mediator.Send(command);
        return NoContent();
    }

    // DELETE: api/users/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteUserCommand { Id = id };

        await _mediator.Send(command);

        return NoContent();
    }
}
