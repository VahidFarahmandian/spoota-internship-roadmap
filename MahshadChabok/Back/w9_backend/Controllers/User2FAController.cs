using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using w9_backend.DTO;
using w9_backend.JWT;
using w9_backend.Model;

namespace w9_backend.Controllers
{
    public class User2FAController : ControllerBase
    {
        private readonly UserContext _context;
        public User2FAController(UserContext context)
        {
            _context = context;
        }
        [HttpPost("Signup2FA")]
        public IActionResult Signup2FA([FromBody] User2FASignup userDto)
        {
            if (userDto == null)
            {
                return BadRequest("Invalid request body");
            }

            if (_context.User2FAs.Any(u => u.Username == userDto.Username))
            {
                return Conflict("Username already exists");
            }

            if (userDto.Username.Length < 6)
            {
                return BadRequest("Username should have at least 6 characters");
            }

            if (userDto.password.Length < 6)
            {
                return BadRequest("Password should have at least 6 characters");
            }

            if (userDto.PhoneNumber.Length != 11)
            {
                return BadRequest("Incorrect PhoneNumber");
            }

            var user = new User2FA
            {
                Username = userDto.Username,
                PhoneNumber = userDto.PhoneNumber,
                Name = userDto.Name,
                password = userDto.password,
                FavoriteColor = userDto.FavoriteColor,
                height = userDto.height
            };
            _context.User2FAs.Add(user);
            _context.SaveChanges();
            return Ok("User2fa registered successfully");
        }

        [HttpPost("Login2FA")]
        public IActionResult Login2FA([FromBody] User2FALogin userDto)
        {
            if (userDto == null)
            {
                return BadRequest("Invalid request body");
            }

            User2FA uS = _context.User2FAs.FirstOrDefault(u => u.Username == userDto.Username);
            if (uS == null)
            {
                return BadRequest("Invalid username");
            }

            if (uS.password != userDto.password)
            {
                return BadRequest("Invalid password");
            }

            // Optionally, you may want to check additional conditions such as phone number match here.

            var token = MakingToken.GenerateJwtToken2FA(uS);

            return Ok( token);
        }
        [Authorize]
        [HttpPost("SmsChecking2FA")]
        public IActionResult SmsChecking2FA([FromBody] NumberDTO dto)
        {
            if (dto.Number != 11111)
            {
                return BadRequest("Invalid enter number");
            }

            return Ok("Correct Number, login successfully");
        }

        [Authorize]
        [HttpPost("CheckBiometricData2FA")]
        public IActionResult CheckNextLayer([FromBody] User2FASecondLayer dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid request body");
            }
            User2FA uS = _context.User2FAs.FirstOrDefault(u => u.Username == dto.Username);
            if (uS == null)
            {
                return BadRequest("Do not have this username");
            }
            if (uS.FavoriteColor == dto.FavoriteColor && uS.height == dto.height)
            {
                return Ok(uS);
            }
            else
            {
                return BadRequest("Your Information Is Not Correct");
            }
        }
        [Authorize]
        [HttpPost("EditProfile2FA")]
        public IActionResult EditProfile2FA([FromBody] User2FASignup userDto)
        {
            if (userDto == null)
            {
                return BadRequest("Invalid request body");
            }

            // Retrieve the user from the database based on the username
            User2FA user = _context.User2FAs.FirstOrDefault(u => u.Username == userDto.Username);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            // Update the user's profile information
            user.Name = userDto.Name;
            user.password = userDto.password;
            user.Username = userDto.Username;
            user.FavoriteColor = userDto.FavoriteColor; 
            user.height = userDto.height;
            user.PhoneNumber = userDto.PhoneNumber;
            _context.SaveChanges();

            return Ok("Profile updated successfully");
        }
    }
}
