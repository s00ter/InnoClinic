using InnoClinic.Shared.Constants;
using Microsoft.IdentityModel.JsonWebTokens;

namespace InnoClinic.AppointmentApi.Api.Middlewares;

public class ParseAuthTokenMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next ?? throw new ArgumentNullException(nameof(next));

    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();

        if (token != null)
        {
            var jsonWebTokenHandler = new JsonWebTokenHandler();

            if (!jsonWebTokenHandler.CanReadToken(token))
            {
                throw new Exception("Can't read token");
            }

            var webToken = jsonWebTokenHandler.ReadJsonWebToken(token);

            context.Request.Headers.Append(CustomClaimTypes.Username, webToken.Claims.First(c => c.Type == CustomClaimTypes.Username).Value);
            context.Request.Headers.Append(CustomClaimTypes.UserId, webToken.Claims.First(c => c.Type == CustomClaimTypes.UserId).Value);

            if (webToken.Claims.Any(c => c.Type == CustomClaimTypes.Role))
            {
                context.Request.Headers.Append(CustomClaimTypes.Role, webToken.Claims.First(c => c.Type == CustomClaimTypes.Role).Value);
            }
        }

        await _next.Invoke(context);
    }
}