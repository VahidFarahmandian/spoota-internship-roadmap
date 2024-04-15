using static UserManagement.API.Model.Response.Responses;
using UserManagement.API.Model.User;

namespace UserManagement.API.Repository.User
{
    public interface IUserAccount
    {
        Task<GeneralResponse> CreateUserAccountAsync(RegisterDTO userDTO);
        Task<GeneralResponse> CreateAdminAccountAsync(RegisterDTO userDto);
        Task<LoginResponse> LoginAccountAsync(LoginDTO loginDTO);
    }
}
