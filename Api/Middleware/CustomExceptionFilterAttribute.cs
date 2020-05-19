using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Api.Middlewares
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<CustomExceptionFilterAttribute> _logger;

        public CustomExceptionFilterAttribute(ILogger<CustomExceptionFilterAttribute> logger)
        {
            _logger = logger;
        }
        
        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception.Demystify(), $"Caught an exception in {nameof(CustomExceptionFilterAttribute)}");
            
            base.OnException(context);
        }
    }
}