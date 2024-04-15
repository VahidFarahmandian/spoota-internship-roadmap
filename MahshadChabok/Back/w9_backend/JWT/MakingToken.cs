using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using w9_backend.DTO;
using w9_backend.Model;

namespace w9_backend.JWT
{
    public class MakingToken
    {
        public static ReturnLogin GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            string s = "Usermanagmentweek9143";
            var key = Encoding.ASCII.GetBytes(s);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Name, user.Username),

                }),
                Expires = DateTime.UtcNow.AddDays(7), // Token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string tok= tokenHandler.WriteToken(token);
            ReturnLogin rl=new ReturnLogin();
            rl.user = user;
            rl.token = tok;
            return rl;
        }
    
    public static ReturnLogin2FA GenerateJwtToken2FA(User2FA user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        string s = "Usermanagmentweek9143";
        var key = Encoding.ASCII.GetBytes(s);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.Name, user.Username),

            }),
            Expires = DateTime.UtcNow.AddDays(7), // Token expiration time
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        string tok = tokenHandler.WriteToken(token);
        ReturnLogin2FA rl = new ReturnLogin2FA();
        rl.user = user;
        rl.token = tok;
        return rl;
    }
        public static ReturnLoginOIDC GenerateJwtTokenOIDC(UserOIDC user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            string s = "Usermanagmentweek9143";
            var key = Encoding.ASCII.GetBytes(s);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Name, user.Username),

                }),
                Expires = DateTime.UtcNow.AddDays(7), // Token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string tok = tokenHandler.WriteToken(token);
            ReturnLoginOIDC rl = new ReturnLoginOIDC();
            rl.User = user;
            rl.token = tok;
            return rl;
        }
    }
}
