using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using AniMedia.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

namespace AniMedia.Infrastructure.Middlewares;

public class AuthorizationResultMiddleware : IAuthorizationMiddlewareResultHandler {
    private const string BearerWithSpace = JwtBearerDefaults.AuthenticationScheme + " ";
    private readonly AuthorizationMiddlewareResultHandler _defaultHandler;
    private readonly IApplicationDbContext _context;

    public AuthorizationResultMiddleware(IApplicationDbContext context) {
        _defaultHandler = new();
        _context = context;
    }

    public async Task HandleAsync(
        RequestDelegate next,
        HttpContext context,
        AuthorizationPolicy policy,
        PolicyAuthorizationResult authorizeResult) {
        var bearerToken = context.Request.Headers.Authorization.ToString();

        if (authorizeResult.Challenged ||
            string.IsNullOrEmpty(bearerToken) ||
            bearerToken.StartsWith(BearerWithSpace) == false) {
            await WriteUnauthorized(context);
            return;
        }

        /// Check session

        var jwt = bearerToken.Substring(BearerWithSpace.Length);

        var containsJwt = await _context.Sessions.AnyAsync(e => e.AccessToken.Equals(jwt));

        if (containsJwt == false) {
            await WriteUnauthorized(context);
            return;
        }

        await _defaultHandler.HandleAsync(next, context, policy, authorizeResult);
    }

    private async Task WriteUnauthorized(HttpContext context) {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await context.Response.WriteAsJsonAsync(new { Message = "Unauthorized" });
    }
}