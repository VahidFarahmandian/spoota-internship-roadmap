using FirstWeb.API.Model.DTO;
using FirstWeb.API.Repositories.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserAccount userAccount;
        public AccountController(IUserAccount _userAccount)
        {
            this.userAccount = _userAccount;
        }

        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterUser(UserDTO userDTO)
        {
            var response = await userAccount.CreateUserAccount(userDTO);
            return Ok(response);
        }

        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin(UserDTO userDTO)
        {
            var response = await userAccount.CreateAdminAccount(userDTO);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var response = await userAccount.LoginAccount(loginDTO);
            return Ok(response);
        }
    }
}
