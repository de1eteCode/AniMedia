using AniMedia.Application.ApiCommands.Binary;
using AniMedia.Application.ApiQueries.Binary;
using AniMedia.Domain.Models.BinaryFiles.Dtos;
using AniMedia.Domain.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AniMedia.API.Controllers.V1;

public class MediaController : BaseApiV1Controller {

    public MediaController(IMediator mediator) : base(mediator) {
    }

    [HttpGet("info/{uid:guid}")]
    [ProducesResponseType(typeof(BinaryFileDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(EntityNotFoundError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetInfo(Guid uid, CancellationToken cancellationToken) {
        var query = new GetBinaryFileQueryCommand(uid);

        return await RequestAsync(query, cancellationToken);
    }

    [HttpGet("file/{uid:guid}")]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(EntityNotFoundError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetFile(Guid uid, CancellationToken cancellationToken) {
        var query = new GetBinaryFileResponseQueryCommand(uid);

        var res = await _mediator.Send(query, cancellationToken);

        if (res.IsSuccess == false) {
            return GenerateResponse(res);
        }

        var entity = res.Value!;

        var stream = System.IO.File.OpenRead(entity.PathFile);

        return File(stream, entity.ContentType, entity.Name, enableRangeProcessing: true);
    }

    [HttpPost("load")]
    [ProducesResponseType(typeof(BinaryFileDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Load(IFormFile file, CancellationToken cancellationToken) {
        var stream = file.OpenReadStream();

        if (stream == null) {
            throw new NotImplementedException();
        }

        var request = new SaveBinaryFileCommand(stream, file.FileName, file.ContentType);

        return await RequestAsync(request, cancellationToken);
    }
}