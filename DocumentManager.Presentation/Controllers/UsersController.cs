using DocumentManager.Application.Users.Commands.CreateUser;
using DocumentManager.Application.Users.Commands.DeleteUser;
using DocumentManager.Application.Users.Commands.UpdateUser;
using DocumentManager.Application.Users.Queries.GetAllUsers;
using DocumentManager.Application.Users.Queries.GetUserById;
using DocumentManager.Presentation.Controllers;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Document_Manager.Presentation.Controllers;

public class UsersController : ApiController
{
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserById(int id, [FromHeader(Name = "X-User-Id")] int userId)
    {
        var query = new GetUserByIdQuery(id, userId);

        var user = await Sender.Send(query);

        return Ok(user);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UserResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromHeader(Name = "X-User-Id")] int userId)
    {
        var users = await Sender.Send(new GetAllUsersQuery(userId));
        return Ok(users);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request, [FromHeader(Name = "X-User-Id")] int userId)
    {
        var command = new CreateUserCommand(request.Email, userId);
        var newUserId = await Sender.Send(command);

        return CreatedAtAction(nameof(GetUserById), new { id = newUserId }, newUserId);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserRequest request, [FromHeader(Name = "X-User-Id")] int userId)
    {
        var command = new UpdateUserCommand(id, request.Email, request.OrganizationId, userId);

        await Sender.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, [FromHeader(Name = "X-User-Id")] int userId)
    {
        var command = new DeleteUserCommand(id, userId);

        await Sender.Send(command);

        return NoContent();
    }
}
