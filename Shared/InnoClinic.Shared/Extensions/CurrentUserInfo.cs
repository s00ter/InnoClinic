using System.Security.Claims;
using InnoClinic.Shared.Constants;
using Microsoft.AspNetCore.Http;

namespace InnoClinic.Shared.Extensions;

public class CurrentUserInfo(IHttpContextAccessor httpContextAccessor) : ICurrentUserInfo
{
    public string GetUserId()
    {
        return httpContextAccessor?.HttpContext?.User.FindFirstValue(CustomClaimTypes.UserId) 
               ?? throw new ArgumentException("Unable to retrieve user id");
    }
}