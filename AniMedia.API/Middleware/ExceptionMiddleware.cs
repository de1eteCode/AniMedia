using AniMedia.Identity.Exceptions;
using System.Net;
using System.Text.Json;

namespace AniMedia.API.Middleware;

internal class ExceptionMiddleware {
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next) {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context) {
        try {
            await _next(context);
        }
        catch (Exception ex) {
            context.Response.ContentType = "application/json";

            HttpStatusCode statusCode;
            string result = string.Empty;

            switch (ex) {
                case IdentityException identityEx:
                    statusCode = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(new {
                        Error = identityEx.Errors
                    });
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    result = JsonSerializer.Serialize(new {
                        Error = ex.Message
                    });
                    break;
            }

            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsync(result);
        }
    }
}