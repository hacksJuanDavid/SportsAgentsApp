using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsAgentsUserService.Api.Dto;
using SportsAgentsUserService.Application.Interfaces;
using SportsAgentsUserService.Domain.Entities;

namespace SportsAgentsUserService.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    // Vars
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    // Constructor
    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    // GET: api/user
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllUsersAsync();
        var usersDto = _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users);
        return Ok(usersDto);
    }

    // GET: api/user/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        var userDto = _mapper.Map<User, UserDto>(user!);
        return Ok(userDto);
    }

    // GET: api/user/email
    [HttpGet("email")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var user = await _userService.GetUserByEmailAsync(email);
        var userDto = _mapper.Map<User, UserDto>(user);
        return Ok(userDto);
    }

    // POST: api/user
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        var userCreated = await _userService.AddUserAsync(user);
        var userCreatedDto = _mapper.Map<User, UserDto>(userCreated);
        return Ok(userCreatedDto);
    }

    // PUT: api/user/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        user.Id = id;
        var userUpdated = await _userService.UpdateUserAsync(user);
        var userUpdatedDto = _mapper.Map<User, UserDto>(userUpdated!);
        return Ok(userUpdatedDto);
    }

    // DELETE: api/user/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _userService.DeleteUserAsync(id);
        return Ok();
    }
}