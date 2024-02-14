using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using NetProject.Data;
using NetProject.Dto;
using NetProject.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly IDistributedCache _cache;
    private readonly IMapper _mapper;

    public UserController(UserDbContext context, IConfiguration configuration, IDistributedCache cache, IMapper mapper)
    {
        _context = context;
        _configuration = configuration;
        _cache = cache;
        _mapper = mapper;
    }

    [HttpPost("Add")]
    public IActionResult AddUser([FromBody] UserDto userDto)
    {
        if (userDto == null)
        {
            // Return BadRequest if the request body is empty or invalid
            return BadRequest("Invalid user data");
        }

        User user = _mapper.Map<User>(userDto);

        _context.MyProperty.Add(user);
        _context.SaveChanges();

        return Ok("User added successfully");
    }

    [HttpGet("Get/{id}")]
    public IActionResult GetUser(int id)
    {
        var cachedUserData = _cache.GetString($"User_{id}");

        if (!string.IsNullOrEmpty(cachedUserData))
        {
            return Ok(cachedUserData);
        }

        var user = _context.MyProperty.FirstOrDefault(u => u.Id == id);

        if (user == null)
        {
            return NotFound($"User with id {id} not found");
        }

        UserDto userDto = _mapper.Map<UserDto>(user);

        var serializedUserData = JsonConvert.SerializeObject(userDto);
        var cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
        };
        _cache.SetString($"User_{id}", serializedUserData, cacheOptions);

        return Ok(userDto);
    }

    [HttpGet("GetAllDapper")]
    public async Task<ActionResult<List<User>>> GetAllUsersWithDapper()
    {
        using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection2"));
        var users = await connection.QueryAsync<User>("select * from MyProperty");
        return Ok(users);
    }

    [HttpDelete("Delete/{id}")]
    public IActionResult DeleteUser(int id)
    {
        var userToRemove = _context.MyProperty.FirstOrDefault(u => u.Id == id);
        if (userToRemove == null)
        {
            return NotFound($"User with id {id} not found");
        }

        _context.MyProperty.Remove(userToRemove);
        _context.SaveChanges();
        return Ok($"User with id {id} deleted successfully");
    }

    [HttpPut("Update/{id}")]
    public IActionResult UpdateUser(int id, [FromBody] UserDto userDto)
    {
        var existingUser = _context.MyProperty.FirstOrDefault(u => u.Id == id);

        if (existingUser == null)
        {
            return NotFound($"User with id {id} not found");
        }

        _mapper.Map(userDto, existingUser);

        _context.SaveChanges();

        return Ok($"User with id {id} updated successfully");
    }
}
