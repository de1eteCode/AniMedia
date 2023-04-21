using AniMedia.Application.ApiCommands.AnimeSeries;
using AniMedia.Application.ApiCommands.RateAnimeSeries;
using AniMedia.Application.ApiQueries.AnimeSeries;
using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AniMedia.API.Controllers.V1; 

public class AnimeSeriesController : BaseApiV1Controller {

    private readonly ICurrentUserService _currentUserService;
    
    public AnimeSeriesController(IMediator mediator, ICurrentUserService currentUserService) : base(mediator) {
        _currentUserService = currentUserService;
    }

    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AnimeSeriesDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(
        [FromQuery] int page,
        [FromQuery] int pageSize,
        CancellationToken cancellationToken) {
        var query = new GetAnimeSeriesListQueryCommand(page, pageSize);

        return await RequestAsync(query, cancellationToken);
    }

    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(AnimeSeriesDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(EntityNotFoundError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(
        [FromQuery] Guid uid,
        CancellationToken cancellationToken) {
        var query = new GetAnimeSeriesQueryCommand(uid);

        return await RequestAsync(query, cancellationToken);
    }
    
    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(AnimeSeriesDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Add(
        [FromBody] string name,
        [FromBody] string englishName,
        [FromBody] string japaneseName,
        [FromBody] string description,
        [FromBody] IEnumerable<GenreDto> genres,
        [FromBody] DateTime? dateOfRelease,
        [FromBody] DateTime? dateOfAnnouncement,
        [FromBody] int? existTotalEpisodes,
        [FromBody] int? planedTotalEpisodes,
        CancellationToken cancellationToken) {
        var request = new AddAnimeSeriesCommand {
            Name = name,
            EnglishName = englishName,
            JapaneseName = japaneseName,
            Description = description,
            Genres = genres,
            DateOfRelease = dateOfRelease,
            DateOfAnnouncement = dateOfAnnouncement,
            ExistTotalEpisodes = existTotalEpisodes,
            PlanedTotalEpisodes = planedTotalEpisodes
        };

        return await RequestAsync(request, cancellationToken);
    }

    [Authorize]
    [HttpPost("{uid:guid}")]
    [ProducesResponseType(typeof(AnimeSeriesDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(EntityNotFoundError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        [FromQuery] Guid uid,
        [FromBody] string name,
        [FromBody] string englishName,
        [FromBody] string japaneseName,
        [FromBody] string description,
        [FromBody] IEnumerable<GenreDto> genres,
        [FromBody] DateTime? dateOfRelease,
        [FromBody] DateTime? dateOfAnnouncement,
        [FromBody] int? existTotalEpisodes,
        [FromBody] int? planedTotalEpisodes,
        CancellationToken cancellationToken) {

        var request = new UpdateAnimeSeriesCommand{
            Uid = uid,
            Name = name,
            EnglishName = englishName,
            JapaneseName = japaneseName,
            Description = description,
            Genres = genres,
            DateOfRelease = dateOfRelease,
            DateOfAnnouncement = dateOfAnnouncement,
            ExistTotalEpisodes = existTotalEpisodes,
            PlanedTotalEpisodes = planedTotalEpisodes
        };

        return await RequestAsync(request, cancellationToken);
    }

    [Authorize]
    [HttpPost("rate/{uidAnimeSeries:guid}")]
    [ProducesResponseType(typeof(RateAnimeSeriesDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(EntityNotFoundError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RateAnimeSeries(
        [FromQuery] Guid uidAnimeSeries,
        [FromBody] byte rate, 
        CancellationToken cancellationToken) {
        var request = new RateAnimeSeriesCommand((Guid)_currentUserService.UserUID!, uidAnimeSeries, rate);

        return await RequestAsync(request, cancellationToken);
    }
}