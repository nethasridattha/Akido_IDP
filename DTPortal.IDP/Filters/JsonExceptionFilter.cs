using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace AppShieldRestAPICore.Filters
{
    public class JsonExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is JsonException ||
                context.Exception is JsonSerializationException)
            {
                context.Result = new BadRequestObjectResult(new
                {
                    success = false,
                    message = "Invalid JSON payload",
                    detail = context.Exception.Message
                });

                context.ExceptionHandled = true;
            }
        }
    }
}
