using Microsoft.AspNetCore.Mvc;

namespace AniMedia.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public abstract class BaseApiV1Controller : ControllerBase {
}