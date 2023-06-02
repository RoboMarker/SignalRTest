using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using SignalRServer.Models;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace SignalRServer.auth
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class JwtAuthenticationManager
    {
        private readonly IConfiguration Configuration;
        public JwtAuthenticationManager(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public string Authenticate(User user)
        {
            var issuer = Configuration.GetValue<string>("JwtSettings:Issuer");//表示 Issuer，發送 Token 的發行者
            var signKey = Configuration.GetValue<string>("JwtSettings:SignKey");
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(signKey);//加密

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),//以使用者的 ID 屬性值為值，創建了一個表示使用者標識的聲明。
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),//生成一個唯一的識別碼作為聲明值，這個聲明可以用於防止 JWT Token 被重複使用。
                new Claim(ClaimTypes.Name, user.UserId),
                new Claim(ClaimTypes.Role, user.PermissionsId.ToString()),
            };

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),//逾期時間
                //使用雜湊運算打亂結果
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            //傳回token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
