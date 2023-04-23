using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AniMedia.Application.ApiQueries.AnimeSeries;

/// <summary>
/// Получение списка аниме сериалов
/// </summary>
/// <param name="Page">Страница</param>
/// <param name="PageSize">Размер страницы</param>
public record GetAnimeSeriesListQueryCommand(int Page, int PageSize) : IRequest<Result<PagedResult<AnimeSeriesDto>>>;

public class GetAnimeSeriesListQueryCommandHandler : IRequestHandler<GetAnimeSeriesListQueryCommand, Result<PagedResult<AnimeSeriesDto>>> {
    private readonly IApplicationDbContext _context;

    public GetAnimeSeriesListQueryCommandHandler(IApplicationDbContext context) {
        _context = context;
    }

    public async Task<Result<PagedResult<AnimeSeriesDto>>> Handle(GetAnimeSeriesListQueryCommand request, CancellationToken cancellationToken) {
        var list = await ResultExtensions.CreatePagedResultAsync(
            _context.AnimeSeries
                .Include(e => e.Genres)
                .OrderByDescending(e => e.LastModified)
                .ThenByDescending(e => e.CreateAt)
                .Select(e => new AnimeSeriesDto(e)),
            request.Page,
            request.PageSize);

        return new Result<PagedResult<AnimeSeriesDto>>(list);
    }
}

public class GetAnimeSeriesListQueryCommandValidator : AbstractValidator<GetAnimeSeriesListQueryCommand> {

    public GetAnimeSeriesListQueryCommandValidator() {
        RuleFor(e => e.Page).GreaterThanOrEqualTo(1);
        RuleFor(e => e.PageSize).GreaterThanOrEqualTo(1);
    }
}