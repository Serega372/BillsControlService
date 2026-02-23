using BillsControl.ApplicationCore.Abstract;
using BillsControl.ApplicationCore.Dtos;
using BillsControl.ApplicationCore.Entities;
using BillsControl.ApplicationCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillsControl.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(IUsersService usersService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest registerUserRequest)
    {
        await usersService.Register(registerUserRequest);
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("admin/register")]
    public async Task<IActionResult> RegisterAsAdmin([FromBody] RegisterUserRequest registerUserRequest)
    {
        await usersService.Register(registerUserRequest, true);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest loginUserRequest)
    {
        var token = await usersService.Login(loginUserRequest);
        HttpContext.Response.Cookies.Append("aboba", token);
        return Ok();
    }

    // [HttpGet("users/{id:guid}")]
    // public async Task<ActionResult<UsersResponse>> GetByUserId(Guid id)
    // {
    //     var user = await usersService.
    // }
}