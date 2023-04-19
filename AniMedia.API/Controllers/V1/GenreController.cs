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

    /// <summary>
    /// Получение жанров
    /// </summary>
    /// <param name="page">Страница</param>
    /// <param name="pageSize">Размер страницы</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список жанров</returns>
    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(GenreDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(int page, int pageSize, CancellationToken cancellationToken) {
        var request = new GetGenresListQueryCommand(page, pageSize);

        return await RequestAsync(request, cancellationToken);
    }

    /// <summary>
    /// Добавление жанра
    /// </summary>
    /// <param name="name">Наименование нового жанра</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Добавленный объект</returns>
    [HttpPost]
    [ProducesResponseType(typeof(GenreDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Add(string name, CancellationToken cancellationToken) {
        var request = new AddGenreCommand(name);

        return await RequestAsync(request, cancellationToken);
    }

    /// <summary>
    /// Обновление жанра
    /// </summary>
    /// <param name="uid">Идентификатор жанра</param>
    /// <param name="name">Новое наименование</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Обновленный объект</returns>
    [HttpPut("{uid:guid}")]
    [ProducesResponseType(typeof(GenreDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(EntityNotFoundError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromQuery] Guid uid, [FromBody] string name, CancellationToken cancellationToken) {
        var request = new UpdateGenreCommand(uid, name);

        return await RequestAsync(request, cancellationToken);
    }
}