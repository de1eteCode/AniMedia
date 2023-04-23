using AniMedia.Application.ApiCommands.Genres;
using AniMedia.Application.ApiQueries.Genres;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AniMedia.API.Controllers.V1; 

[Authorize]
public class GenreController : BaseApiV1Controller {

    public GenreController(IMediator mediator) : base(mediator) {
    }

    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<GenreDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(int page, int pageSize, CancellationToken cancellationToken) {
        var request = new GetGenresListQueryCommand(page, pageSize);

        return await RequestAsync(request, cancellationToken);
    }
    
    [AllowAnonymous]
    [HttpGet("id/{genreUid:guid}")]
    [ProducesResponseType(typeof(GenreDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(Guid genreUid, CancellationToken cancellationToken) {
        var request = new GetGenreQueryCommand(genreUid);

        return await RequestAsync(request, cancellationToken);
    }

    [HttpPost]
    [ProducesResponseType(typeof(GenreDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Add(string name, CancellationToken cancellationToken) {
        var request = new AddGenreCommand(name);

        return await RequestAsync(request, cancellationToken);
    }

    [HttpPut("{uid:guid}")]
    [ProducesResponseType(typeof(GenreDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(EntityNotFoundError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid uid, string name, CancellationToken cancellationToken) {
        var request = new UpdateGenreCommand(uid, name);

        return await RequestAsync(request, cancellationToken);
    }
}