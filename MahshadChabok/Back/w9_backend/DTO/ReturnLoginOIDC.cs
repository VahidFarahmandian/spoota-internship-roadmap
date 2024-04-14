using w9_backend.Model;

namespace w9_backend.DTO
{
    public class ReturnLoginOIDC

    {
        public UserOIDC User { get; set; }  
        public string  token { get; set; }  
    }
}
