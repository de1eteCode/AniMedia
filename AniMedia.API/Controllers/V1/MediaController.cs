using AniMedia.Application.ApiCommands.Binary;
using AniMedia.Application.ApiQueries.Binary;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AniMedia.API.Controllers.V1;

public class MediaController : BaseApiV1Controller {
    
    public MediaController(IMediator mediator) : base(mediator) {
    }

    [HttpGet("info/{uidOrName}")]
    [ProducesResponseType(typeof(BinaryFileDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(EntityNotFoundError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetInfo(string uidOrName, CancellationToken cancellationToken) {
        var query = new GetBinaryFileQueryCommand(uidOrName);

        return await RequestAsync(query, cancellationToken);
    }

    [HttpGet("file/{uidOrName}")]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(EntityNotFoundError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetFile(string uidOrName, CancellationToken cancellationToken) {
        var query = new GetBinaryFileResponseQueryCommand(uidOrName);

        var res = await _mediator.Send(query, cancellationToken);

        if (res.IsSuccess == false) {
            return GenerateResponse(res);
        }

        var entity = res.Value!;

        return PhysicalFile(entity.PathFile, entity.ContentType, entity.Name, enableRangeProcessing: true);
    }

    [HttpPost("load")]
    [ProducesResponseType(typeof(BinaryFileDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Load(IFormFile file, CancellationToken cancellationToken) {
        var stream = file.OpenReadStream();

        if (stream == null) {
            throw new NotImplementedException();
        }

        var request = new SaveBinaryFileCommand(stream, file.ContentType);

        return await RequestAsync(request, cancellationToken);
    }
}