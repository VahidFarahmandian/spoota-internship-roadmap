using FirstWeb.API.Model.DTO;
using static FirstWeb.API.Model.DTO.Responses;

namespace FirstWeb.API.Repositories.Account
{
    public interface IUserAccount
    {
        Task<GeneralResponse> CreateUserAccount(UserDTO userDTO);
        Task<GeneralResponse> CreateAdminAccount(UserDTO userDto);
        Task<LoginResponse> LoginAccount(LoginDTO loginDTO);
    }
}
