using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace AniMedia.API.Controllers.V1;

[Authorize]
[Route("account")]
public class AccountController : BaseApiV1Controller {

    public AccountController(IMediator mediator) : base(mediator) {
    }
}