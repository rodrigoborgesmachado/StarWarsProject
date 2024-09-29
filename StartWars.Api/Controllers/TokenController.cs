using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StartWars.Api.Requests;
using StarWars.Domain.Settings;
using System.Security.Claims;

namespace StartWars.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController
    {
        private readonly Settings _settings;
        private readonly Handlers.TokenHandler _tokenHandler;
        private readonly IConfiguration _builder;

        public TokenController(IOptions<Settings> options, IHttpContextAccessor httpContextAccessor, IConfiguration builder)
        {
            _settings = options.Value;
            _tokenHandler = new Handlers.TokenHandler(httpContextAccessor);
            _builder = builder;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IResult> TokenAsync(GetTokenRequest request)
        {
            var usernameSecret = _settings.UserName;
            var tokenSecret = _settings.Token;

            if (request.UserName.Equals(usernameSecret))
            {
                if (!request.Token.Equals(tokenSecret))
                {
                    return Results.BadRequest(new
                    {
                        error = "invalid_grant",
                        error_description = "The user name or password is incorrect."
                    });
                }

                var claims = new[]
                {
                    new Claim("Code", "0"),
                    new Claim("Name", $"Admin"),
                    new Claim("IsAdmin", "1")
                };

                Token result = _tokenHandler.CreateToken(claims, _builder);

                return Results.Ok(result);
            }
            else 
            {
                return Results.BadRequest(new
                {
                    error = "invalid_grant",
                    error_description = "The user name doesn't exists."
                });
            }
        }

    }
}
