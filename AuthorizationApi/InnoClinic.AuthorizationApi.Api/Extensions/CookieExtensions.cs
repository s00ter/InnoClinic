namespace InnoClinic.Authorization.Extensions;

public static class CookieExtensions
{
    public static void SetAuthCookie(this IResponseCookies cookies, string key, string value, DateTimeOffset expires)
    {
        cookies.Append(key, value, new CookieOptions
        {
            Expires = expires,
            HttpOnly = true,
            IsEssential = true,
            Secure = true,
            SameSite = SameSiteMode.None
        });
    }
}