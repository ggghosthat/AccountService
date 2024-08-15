using AccountService.API.Requests;
using AccountService.API.Routes;
using AccountService.Entity;
using AccountService.Contracts.Requests;
using AccountService.API.ActionFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace AccountService.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Get user by id.
    /// </summary>
    /// <param name="id">User id.</param>
    /// <returns>Found users.</returns>
    [HttpGet(ApiRoutes.User.GetUserById, Name="GetUserById")]
    public async Task<ActionResult<User>> GetUserById([FromRoute]Guid id)
    {
        var userGetByIdRequest = new UserGetByIdRequest(id);
        var user = await _mediator.Send(userGetByIdRequest);

        if (user != null)
            return Ok(user);
        else 
            return NotFound();
    }

    /// <summary>
    /// Search users by parameters.
    /// </summary>
    /// <param name="userParameters">User parameters.</param>
    /// <returns>Found users.</returns>
    [HttpGet(ApiRoutes.User.SearchUsers)]
    public async Task<IActionResult> SearchUsers([FromQuery]UserParameters userParameters)
    {
        var userSearchRequest = new UserSearchRequest(userParameters);
        var searchResponse = await _mediator.Send(userSearchRequest);
        return Ok(searchResponse);
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="user">User to create.</param>
    /// <returns>A newly created resource.</returns>
    [HttpPost]
    [ServiceFilter(typeof(ValidationFilter))]
    public async Task<IActionResult> CreateUser([FromBody] UserDto user)
    {
        if(!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        var userCreateRequest = new UserCreateRequest(user);
        var userCreated = await _mediator.Send(userCreateRequest);
        return CreatedAtAction("GetUserById", new { id = userCreated.Id }, userCreated);
    }

    /// <summary>
    /// Delete user by id.
    /// </summary>
    /// <param name="id">User id.</param>
    /// <returns>User deleted.</returns>
    [HttpDelete(ApiRoutes.User.DeleteUser)]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var userDeleteRequest = new UserDeleteRequest(id);
        await _mediator.Send(userDeleteRequest);
        return NoContent();
    }
}