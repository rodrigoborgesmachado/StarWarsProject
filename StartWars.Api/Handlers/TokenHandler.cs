using Microsoft.IdentityModel.Tokens;
using StarWars.Domain.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StartWars.Api.Handlers
{
    public class TokenHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TokenHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Token CreateToken(IEnumerable<Claim> claims, IConfiguration _builder)
        {
            var identity = new ClaimsIdentity(claims);

            var key = Encoding.ASCII.GetBytes(_builder.GetValue<string>("Jwt:Key"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature),
                NotBefore = DateTime.Now.AddDays(-1),
                Issuer = _builder.GetValue<string>("Jwt:Issuer"),
                Audience = _builder.GetValue<string>("Jwt:Audience")
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);

            TimeSpan duration = tokenDescriptor.Expires.Value - DateTime.UtcNow;

            Token result = new Token()
            {
                access_token = stringToken,
                IsAdmin = claims.Where(i => i.Type.Equals("IsAdmin")).FirstOrDefault().Value,
                Code = claims.Where(i => i.Type.Equals("Code")).FirstOrDefault().Value,
                Name = claims.Where(i => i.Type.Equals("Name")).FirstOrDefault().Value,
            };

            return result;
        }

    }
}
