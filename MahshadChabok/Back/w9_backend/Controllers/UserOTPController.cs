using Microsoft.AspNetCore.Mvc;
using w9_backend.Model;
using w9_backend.DTO;
using w9_backend.JWT;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace w9_backend.Controllers
{
    public class UserOTPController : ControllerBase
    {
        private readonly UserContext _context;
        public UserOTPController(UserContext context)
        {
            _context = context;
        }
        [HttpPost("SignupOTP")]
        public IActionResult SignupOTP([FromBody] UserSignUp userDto)
        {
            if (userDto == null)
            {
                return BadRequest("Invalid request body");
            }


            if (_context.Users.Any(u => u.Username == userDto.Username))
            {
                return Conflict("Username already exists");
            }

            if (userDto.Username.Length < 6)
            {
                return BadRequest("User Name should have atleast 6 characters");
            }
            if (userDto.PhoneNumber.Length != 11)
            {
                return BadRequest("Incorrect PhoneNumber");
            }
            var user = new User { Username = userDto.Username, PhoneNumber=userDto.PhoneNumber,Name=userDto.Name,Email=userDto.Email };
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok("User registered successfully");
        }

        [HttpPost("LoginOTP")]
        public IActionResult LoginOTP([FromBody] UserLogin userDto)
        {
            if (userDto == null)
            {
                return BadRequest("Invalid request body");
            }

            User uS = _context.Users.FirstOrDefault(u => u.Username == userDto.Username);
            if (uS == null)
            {
                return BadRequest("Invalid username");
            }
           
            if (uS.PhoneNumber != userDto.PhoneNumber) {
               return BadRequest("Not Match Username And PhoneNumber");
            }
           
            var token = MakingToken.GenerateJwtToken(uS);

            return Ok( token);
          
        }
        [Authorize]
        [HttpPost("SmsCheckingOTP")]
        public IActionResult SmsCheckingOTP([FromBody] NumberDTO dto)
        {
            if (dto.Number != 11111)
            {
                return BadRequest("Invalid enter number");
            }

            return Ok("Correct Number, login successfully");
        }
        [Authorize]
        [HttpPost("EditProfileOTP")]
        public IActionResult EditProfileOTP([FromBody] UserSignUp userDto)
        {
            if (userDto == null)
            {
                return BadRequest("Invalid request body");
            }

            // Retrieve the user from the database based on the username
            User user = _context.Users.FirstOrDefault(u => u.Username == userDto.Username);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            // Update the user's profile information
            user.Name = userDto.Name;
            user.Email = userDto.Email;
            user.PhoneNumber = userDto.PhoneNumber;
            user.Username = userDto.Username;   
            _context.SaveChanges();

            return Ok("Profile updated successfully");
        }

    }
    
}
