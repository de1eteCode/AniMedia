using AniMedia.Domain.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AniMedia.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public abstract class BaseApiV1Controller : ControllerBase {
    protected readonly IMediator _mediator;

    protected BaseApiV1Controller(IMediator mediator) {
        _mediator = mediator;
    }

    [NonAction]
    protected async Task<IActionResult> RequestAsync<TValue>(IRequest<Result<TValue>> request,
        CancellationToken cancellationToken) {
        var result = await _mediator.Send(request, cancellationToken);

        return GenerateResponse(result);
    }

    [NonAction]
    protected IActionResult GenerateResponse<TValue>(Result<TValue> result) {
        return result.Error switch {
            AuthenticationError error =>
                new ObjectResult(new { error.Message }) { StatusCode = 401 },

            EntityNotFoundError error =>
                new ObjectResult(new { error.Message }) { StatusCode = 404 },

            _ => new ObjectResult(result.Value) { StatusCode = 200 }
        };
    }
}