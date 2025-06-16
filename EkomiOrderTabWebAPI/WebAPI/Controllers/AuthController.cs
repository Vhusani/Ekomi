using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebAPI.Controllers
{
    public class AuthController : Controller
    {
        private IConfiguration Configuration { get; }
        public AuthController(IConfiguration configuration) {
            Configuration = configuration;
        }

        [HttpGet]
        [Route("auth/{UserId}")]
        public async Task<JsonResult> JsonWebToken(string UserId)
        {
            try
            {
                int expiration = Configuration.GetSection("JwtIssuerOptions:Expires").Get<int>();
                var secretKey = Configuration.GetSection("JwtIssuerOptions:Key").Get<string>();
                var issuer = Configuration.GetSection("JwtIssuerOptions:Issuer").Get<string>();
                var audience = Configuration.GetSection("JwtIssuerOptions:Audience").Get<string>();

                if (UserId is null)
                {
                    var response = new { success = false, message = "User is empty" };
                    return new JsonResult(response);
                }


                var claims = new[] {

                    new Claim("UserId", UserId)

                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var expires = DateTime.Now.AddMinutes(expiration);

                var token = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    expires: expires,
                    signingCredentials: creds
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return new JsonResult(new { success = true, token = tokenString });
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }
    }
}
