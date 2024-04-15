using w9_backend.Model;

namespace w9_backend.DTO
{
    public class ReturnLogin
    {
        public string token { get; set; }
        public User  user { get; set; }
}
}
