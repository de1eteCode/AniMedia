using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AniMedia.API.Middleware;

internal class ExceptionMiddleware : IMiddleware {

    public async Task InvokeAsync(HttpContext context, RequestDelegate next) {
        try {
            await next(context);
        }
        catch (Exception ex) {
            context.Response.ContentType = "application/json";

            var statusCode = HttpStatusCode.InternalServerError;

            var result = JsonSerializer.Serialize(new {
                ErrorType = "Failure",
                ErrorMessage = ex.Message
            });

            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsync(result);
        }
    }
}