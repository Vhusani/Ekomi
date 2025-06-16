using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Services.JsonWebToken
{
    public interface IJsonWebTokenService
    {
        JsonResult GetToken(string UserId);

    }
}
