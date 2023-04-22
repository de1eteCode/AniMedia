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
    [ProducesResponseType(typeof(PagedResult<AnimeSeriesDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(
        [FromQuery] int page,
        [FromQuery] int pageSize,
        CancellationToken cancellationToken) {
        var query = new GetAnimeSeriesListQueryCommand(page, pageSize);

        return await RequestAsync(query, cancellationToken);
    }

    [AllowAnonymous]
    [HttpGet("{uid:guid}")]
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
        [FromBody] AnimeSeriesDto dto,
        CancellationToken cancellationToken) {
        var request = new AddAnimeSeriesCommand {
            Name = dto.Name,
            EnglishName = dto.EnglishName,
            JapaneseName = dto.JapaneseName,
            Description = dto.Description,
            Genres = dto.Genres,
            DateOfRelease = dto.DateOfRelease,
            DateOfAnnouncement = dto.DateOfAnnouncement,
            ExistTotalEpisodes = dto.ExistTotalEpisodes,
            PlanedTotalEpisodes = dto.PlanedTotalEpisodes
        };

        return await RequestAsync(request, cancellationToken);
    }

    [Authorize]
    [HttpPost("{uid:guid}")]
    [ProducesResponseType(typeof(AnimeSeriesDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(EntityNotFoundError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        [FromQuery] Guid uid,
        [FromBody] AnimeSeriesDto dto,
        CancellationToken cancellationToken) {
        // ignore guid anime series in dto
        
        var request = new UpdateAnimeSeriesCommand{
            Uid = uid,
            Name = dto.Name,
            EnglishName = dto.EnglishName,
            JapaneseName = dto.JapaneseName,
            Description = dto.Description,
            Genres = dto.Genres,
            DateOfRelease = dto.DateOfRelease,
            DateOfAnnouncement = dto.DateOfAnnouncement,
            ExistTotalEpisodes = dto.ExistTotalEpisodes,
            PlanedTotalEpisodes = dto.PlanedTotalEpisodes
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