using w9_backend.Model;

namespace w9_backend.DTO
{
    public class ReturnLogin2FA
    {
        public string token { get; set; }
        public User2FA user { get; set; }
    }
}
