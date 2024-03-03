using AutoMapper;
using FirstWeb.API.Data;
using FirstWeb.API.Model.DTO.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static FirstWeb.API.Model.DTO.Response.Responses;


namespace FirstWeb.API.Repositories.Account
{
    public class UserAccount : IUserAccount
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration config;
        private readonly IMapper mapper;
        public UserAccount(
            UserManager<ApplicationUser> _userManager,
            RoleManager<IdentityRole> _roleManager,
            IConfiguration _config,
            IMapper _mapper)
        {
            this.userManager = _userManager;
            this.roleManager = _roleManager;
            this.config = _config;
            this.mapper = _mapper;
        }
        public async Task<GeneralResponse> CreateUserAccount(UserDTO userDTO)
        {
            if (userDTO is null) return new GeneralResponse(false, "Model is Empty");


            var newUser = mapper.Map<ApplicationUser>(userDTO);
            var user = await userManager.FindByEmailAsync(newUser.Email);
            if (user is not null) return new GeneralResponse(false, "User registered already");

            var createUser = await userManager.CreateAsync(newUser, userDTO.Password);
            if (!createUser.Succeeded) return new GeneralResponse(false, "Error occured.. please try again");

            //Assign User Role

            var checkUser = await roleManager.FindByNameAsync("User");
            if (checkUser is null)
                await roleManager.CreateAsync(new IdentityRole() { Name = "User" });

            await userManager.AddToRoleAsync(newUser, "User");
            return new GeneralResponse(true, "Account Created");

        }

        public async Task<GeneralResponse> CreateAdminAccount(UserDTO userDTO)
        {
            if (userDTO is null) return new GeneralResponse(false, "Model is Empty");

            var newUser = mapper.Map<ApplicationUser>(userDTO);
            var user = await userManager.FindByEmailAsync(newUser.Email);
            if (user is not null) return new GeneralResponse(false, "User registered already");

            var createUser = await userManager.CreateAsync(newUser, userDTO.Password);
            if (!createUser.Succeeded) return new GeneralResponse(false, "Error occured.. please try again");

            //Assign User Role

            var checkUser = await roleManager.FindByNameAsync("Admin");
            if (checkUser is null)
                await roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });

            await userManager.AddToRoleAsync(newUser, "Admin");

            return new GeneralResponse(true, "Account Created");
        }

        public async Task<LoginResponse> LoginAccount(LoginDTO loginDTO)
        {
            if (loginDTO == null)
                return new LoginResponse(false, null!, "Login container is empty");

            var getUser = await userManager.FindByEmailAsync(loginDTO.Email);
            if (getUser is null)
                return new LoginResponse(false,null!,"User not found");

            bool checkUserPasswords = await userManager.CheckPasswordAsync(getUser, loginDTO.Password);
            if (!checkUserPasswords)
                return new LoginResponse(false,null!,"Invalid email/password");

            var getUserRole = await userManager.GetRolesAsync(getUser);
            var userSession = new UserSession(getUser.Id, getUser.Name!, getUser.Email!, getUserRole.First());
            string token = GenerateToken(userSession);
            return new LoginResponse(true, token!, "Login completed");

        }

        private string GenerateToken(UserSession user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var creditials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id!),
                new Claim(ClaimTypes.Name,user.Name!),
                new Claim(ClaimTypes.Email,user.Email!),
                new Claim(ClaimTypes.Role,user.Role!),
            };

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims:userClaims,
                expires:DateTime.Now.AddHours(1),
                signingCredentials:creditials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
