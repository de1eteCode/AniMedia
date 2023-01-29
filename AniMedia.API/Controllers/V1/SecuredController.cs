using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AniMedia.API.Controllers.V1;

[Authorize]
public class SecuredController : BaseApiV1Controller {

    [HttpGet]
    public ActionResult<Guid> Get() {
        return Ok(Guid.NewGuid());
    }
}