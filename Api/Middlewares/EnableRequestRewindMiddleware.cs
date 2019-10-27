using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Api.Middlewares
{
    public class EnableRequestRewindMiddleware
    {
        private readonly RequestDelegate _next;

        ///<inheritdoc/>
        public EnableRequestRewindMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        ///     On any action ...
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public async Task Invoke(HttpContext context)
        {
            context.Request.EnableBuffering();

            await _next(context);
        }
    }
}