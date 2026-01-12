using FluentValidation;
using System.Net;
using System.Text.Json;

namespace TMS.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                object response;
                context.Response.StatusCode = ex switch
                {
                    ValidationException => (int)HttpStatusCode.BadRequest,
                    ArgumentException => (int)HttpStatusCode.BadRequest,
                    InvalidOperationException => (int)HttpStatusCode.Conflict,
                    KeyNotFoundException => (int)HttpStatusCode.NotFound,
                    _=> (int)HttpStatusCode.InternalServerError
                };
                if (ex is ValidationException validationEx) {
                    response = new
                    {
                        message = "Validation failed",
                        errors = validationEx.Errors.Select(e => new
                        {
                            field = e.PropertyName,
                            error = e.ErrorMessage
                        })
                    };
                } else {
                    response = new
                    {
                        message = ex.Message,
                        statusCode = context.Response.StatusCode
                    };
                }
                    
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
