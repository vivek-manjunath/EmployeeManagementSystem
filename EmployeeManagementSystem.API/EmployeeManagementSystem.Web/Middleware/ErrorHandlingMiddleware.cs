using System.Net;
using System;
using System.Threading.Tasks;
using EmployeeManagementSystem.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace EmployeeManagementSystem.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ErrorHandlingMiddleware> logger;
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger) =>
            (this.next, this.logger) = (next, logger);

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private async Task HandleException(HttpContext context, Exception ex)
        {
            object errors = null;
            switch (ex)
            {
                case RestException re:
                    logger.LogError(ex, "REST ERROR");
                    context.Response.StatusCode = (int)re.StatusCode;
                    errors = re.Errors;
                    break;
                case Exception e:
                    logger.LogError(ex, "SERVER ERROR");
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errors = String.IsNullOrWhiteSpace(ex.Message) ? "Error" : e.Message;
                    break;
            }

            context.Response.ContentType = "application/json";
            if (errors != null)
            {
                var result = JsonSerializer.Serialize(new { errors });
                await context.Response.WriteAsync(result);
            }
        }
    }
}