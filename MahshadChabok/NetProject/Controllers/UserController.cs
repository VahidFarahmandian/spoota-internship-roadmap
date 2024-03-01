using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using NetProject.Data;
using NetProject.Dto;
using NetProject.model;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NetProject.Service;

[ApiController]
[Route("api/[controller]")]

public class UserController : ControllerBase
{
    private readonly UserDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly IDistributedCache _cache;
    private readonly IMapper _mapper;
    private readonly IRepository _repository;
    private readonly RegisterUserContext _registerContext;
    private readonly PasswordHasher<string> _passwordHasher = new PasswordHasher<string>();

    public UserController(UserDbContext context, IConfiguration configuration, IDistributedCache cache, IMapper mapper, IRepository repository, RegisterUserContext registerContext)
    {
        _context = context;
        _configuration = configuration;
        _cache = cache;
        _mapper = mapper;
        _repository = repository;
        _registerContext = registerContext; 
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterUserDto userDto)
    {
        if (userDto == null)
        {
            return BadRequest("Invalid request body");
        }

        // Check if the username already exists
        if (_registerContext.RegisterUsers.Any(u => u.Username == userDto.Username))
        {
            return Conflict("Username already exists");
        }

        // Hash the password
        string hashedPassword = _passwordHasher.HashPassword(null, userDto.Password);

       
        var newUser = new RegisterUser { Username = userDto.Username, HashedPassword = hashedPassword };
        _registerContext.RegisterUsers.Add(newUser);
        _registerContext.SaveChanges();

        return Ok("User registered successfully");
    }

    [HttpPost("authenticate")]
    public IActionResult Authenticate([FromBody] RegisterUserDto loginDto)
    {
        if (loginDto == null)
        {
            return BadRequest("Invalid request body");
        }

        var user = _registerContext.RegisterUsers.SingleOrDefault(u => u.Username == loginDto.Username);

        if (user == null || !_passwordHasher.VerifyHashedPassword(null, user.HashedPassword, loginDto.Password).Equals(PasswordVerificationResult.Success))
        {
            return Unauthorized("Invalid username or password");
        }

        
        var token = TokenService.GenerateJwtToken(user , _configuration);

        return Ok(new JwtToken { Token = token });
    }
    [Authorize]
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
    [Authorize]
    [HttpGet("Get/{id}")]
    public IActionResult GetUser(int id)
    {
        var cachedUserData = _cache.GetString($"User_{id}");

        if (!string.IsNullOrEmpty(cachedUserData))
        {
            return Ok(cachedUserData);
        }

        var user = _context.MyProperty.Find(id);

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
    [Authorize]
    [HttpGet("GetAllDapper")]
    public async Task<ActionResult<List<User>>> GetAllUsersWithDapper()
    {
        var users = _repository.GetAllUsers();
        return Ok(users);
    }

    [Authorize]
    [HttpDelete("Delete/{id}")]
    public IActionResult DeleteUser(int id)
    {
        var userToRemove = _context.MyProperty.Find(id);
        if (userToRemove == null)
        {
            return NotFound($"User with id {id} not found");
        }

        _context.MyProperty.Remove(userToRemove);
        _context.SaveChanges();
        return Ok($"User with id {id} deleted successfully");
    }

    [Authorize]
    [HttpPut("Update/{id}")]
    public IActionResult UpdateUser(int id, [FromBody] UserDto userDto)
    {
        var existingUser = _context.MyProperty.Find(id);

        if (existingUser == null)
        {
            return NotFound($"User with id {id} not found");
        }

        _mapper.Map(userDto, existingUser);

        _context.SaveChanges();

        return Ok($"User with id {id} updated successfully");
    }

}
