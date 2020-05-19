using Api.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Api.Extensions
{
    public static class EnableRequestRewindExtension
    {
        public static IApplicationBuilder UseEnableRequestRewind(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<EnableRequestRewindMiddleware>();

            return builder;
        }
    }
}