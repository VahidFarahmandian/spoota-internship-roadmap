
using System.Data.SqlClient;

using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using NetProject.Data;
using NetProject.Dto;
using NetProject.model;
using Newtonsoft.Json;


namespace netproject.controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
       
        private readonly UserDbContext _context;
        private readonly DapperRepository _dapperRepository;
        private readonly IConfiguration _configuration;
        private readonly IDistributedCache _cache;
        public UserController(UserDbContext context, DapperRepository dapperRepository, IConfiguration configuration, IDistributedCache cache)
        {
            _context = context;
            _dapperRepository = dapperRepository;
            _configuration = configuration;
            _cache = cache;
        }

        [HttpPost("Add")]
        public IActionResult AddUser([FromBody] UserDto userDto)
        {
            try
            {
                if (userDto == null)
                {
                    // Return BadRequest if the request body is empty or invalid
                    return BadRequest("Invalid user data");
                }
                User user = new User
                {
                    Name = userDto.Name,
                    LastName = userDto.LastName,
                    Hieght = userDto.Hieght,
                    Age = userDto.Age,
                };

                _context.MyProperty.Add(user);
                _context.SaveChanges();

                return Ok("User added successfully");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error adding user: {ex.Message}");
                return StatusCode(500, "An error occurred while processing the request");
            }
        }

        [HttpGet("Get/{id}")]
        public IActionResult GetUser(int id)
        {
            try
            {
                // Check if the user data is in the cache
                var cachedUserData = _cache.GetString($"User_{id}");

                if (!string.IsNullOrEmpty(cachedUserData))
                {
                    // Data found in the cache, deserialize and return
                    var userDto2 = JsonConvert.DeserializeObject<UserDto>(cachedUserData);
                    return Ok(userDto2);
                }

                // Data not found in the cache, fetch from the database
                var user = _context.MyProperty.FirstOrDefault(u => u.Id == id);

                if (user == null)
                {
                    return NotFound($"User with id {id} not found");
                }

                UserDto userDto = new UserDto
                {
                    Name = user.Name,
                    LastName = user.LastName,
                    Hieght = user.Hieght,
                    Age = user.Age,
                };

                // Serialize userDto and store it in the cache for 30 minutes
                var serializedUserData = JsonConvert.SerializeObject(userDto);
                var cacheOptions = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                };
                _cache.SetString($"User_{id}", serializedUserData, cacheOptions);

                return Ok(userDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving user: {ex.Message}");
                return StatusCode(500, "An error occurred while processing the request");
            }
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
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting user: {ex.Message}");
                return StatusCode(500, "An error occurred while processing the request");
            }
        }
        [HttpPut("Update/{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserDto userDto)
        {
            try
            {
                var existingUser = _context.MyProperty.FirstOrDefault(u => u.Id == id);

                if (existingUser == null)
                {
                    return NotFound($"User with id {id} not found");
                }

                
                existingUser.Name = userDto.Name;
                existingUser.Age = userDto.Age;
                existingUser.LastName = userDto.LastName;
                existingUser.Hieght = userDto.Hieght;
                _context.SaveChanges();


                return Ok($"User with id {id} updated successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating user: {ex.Message}");
                return StatusCode(500, "An error occurred while processing the request");
            }
        }
    }
}
