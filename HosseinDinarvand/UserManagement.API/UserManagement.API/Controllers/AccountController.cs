using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.API.Model.User;
using UserManagement.API.Repository.User;

namespace UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserAccount _userAccount;
        public AccountController(IUserAccount userAccount)
        {
            this._userAccount = userAccount;
        }

        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterUser(RegisterDTO registerDTO)
        {
            var registerResponse = await _userAccount.CreateUserAccountAsync(registerDTO);
            return Ok(registerResponse);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var loginResponse = await _userAccount.LoginAccountAsync(loginDTO);
            return Ok(loginResponse);
        }
    }
}
