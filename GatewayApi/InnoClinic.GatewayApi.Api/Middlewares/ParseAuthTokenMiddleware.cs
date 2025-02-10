using InnoClinic.BusinessLogic.Contants;
using Microsoft.IdentityModel.JsonWebTokens;

namespace InnoClinic.GatewayApi.Api.Middlewares;

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

            /*if (webToken.Claims.Any(c => c.Type == CustomClaimTypes.Roles))
            {
                context.Request.Headers.Append(CustomClaimTypes.Roles, webToken.Claims.First(c => c.Type == CustomClaimTypes.Roles).Value);
            }

            if (webToken.Claims.Any(c => c.Type == CustomClaimTypes.Permissions))
            {
                context.Request.Headers.Append(CustomClaimTypes.Permissions, webToken.Claims.First(c => c.Type == CustomClaimTypes.Permissions).Value);
            }*/
        }

        await _next.Invoke(context);
    }
}