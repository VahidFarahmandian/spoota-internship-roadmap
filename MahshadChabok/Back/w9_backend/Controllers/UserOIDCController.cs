using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using w9_backend.DTO;
using w9_backend.JWT;
using w9_backend.Model;

namespace w9_backend.Controllers
{
    public class UserOIDCController : ControllerBase
    {
        private readonly UserContext _context;
        public UserOIDCController(UserContext context)
        {
            _context = context;
        }
        [HttpPost("SignupOIDC")]
        public IActionResult SignupOIDC([FromBody] UserOIDCSignup userDto)
        {
            if (userDto == null)
            {
                return BadRequest("Invalid request body");
            }


            if (_context.UserOIDCs.Any(u => u.Username == userDto.Username))
            {
                return Conflict("Username already exists");
            }
            if (_context.UserOIDCs.Any(u => u.Password == userDto.Password))
            {
                return Conflict("PASSWORD already exists");
            }

            if (userDto.Username.Length < 6)
            {
                return BadRequest("User Name should have atleast 6 characters");
            }
            if (userDto.Password.Length <6)
            {
                return BadRequest("Incorrect PhoneNumber");
            }
            var user = new UserOIDC { Username = userDto.Username, Name = userDto.Name,Password = userDto.Password };
            _context.UserOIDCs.Add(user);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPost("LoginOIDC")]
        public IActionResult LoginOIDC([FromBody] User2FALogin userDto)
        {
            if (userDto == null)
            {
                return BadRequest("Invalid request body");
            }

            UserOIDC uS = _context.UserOIDCs.FirstOrDefault(u => u.Username == userDto.Username);
            if (uS == null)
            {
                return BadRequest("Invalid username");
            }

            if (uS.Password != userDto.password)
            {
                return BadRequest("Not Match Username And PhoneNumber");
            }

            var token = MakingToken.GenerateJwtTokenOIDC(uS);

            return Ok( token );

        }
        [Authorize]
        [HttpPost("EditProfileOIDC")]
        public IActionResult EditProfileOIDC([FromBody] UserOIDCSignup userDto)
        {
            if (userDto == null)
            {
                return BadRequest("Invalid request body");
            }

            // Retrieve the user from the database based on the username
            UserOIDC user = _context.UserOIDCs.FirstOrDefault(u => u.Username == userDto.Username);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            // Update the user's profile information
            user.Name = userDto.Name;
            user.Password = userDto.Password;
           
            user.Username = userDto.Username;
            _context.SaveChanges();

            return Ok("Profile updated successfully");
        }

    }
}
