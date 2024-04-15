using System.ComponentModel.DataAnnotations;

namespace UserManagement.API.Model.User
{
    public class RegisterDTO
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
    }
}
