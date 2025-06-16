using Dapper;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Models;

namespace WebAPI.Services.JsonWebToken
{
    public class JsonWebTokenService: IJsonWebTokenService
    {

        private IConfiguration Configuration { get; }

        public JsonWebTokenService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public JsonResult GetToken(string UserId)
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

            var claims = new[] { new Claim("UserId", UserId) };

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

            return new JsonResult(tokenString);

        }
    }
}
