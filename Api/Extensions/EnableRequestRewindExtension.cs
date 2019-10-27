using Api.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Api.Extensions
{
    public static class EnableRequestRewindExtension
    {
        public static void UseEnableRequestRewind(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<EnableRequestRewindMiddleware>();
        }
    }
}