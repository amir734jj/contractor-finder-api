using API.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace API.Extensions
{
    public static class EnableRequestRewindExtension
    {
        public static void UseEnableRequestRewind(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<EnableRequestRewindMiddleware>();
        }
    }
}