using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using FluentValidation;
using MediatR;

namespace AniMedia.Application.ApiQueries.Genres;

/// <summary>
/// Получение списка жанров
/// </summary>
/// <param name="Page">Страница</param>
/// <param name="PageSize">Размер страницы</param>
public record GetGenresListQueryCommand(int Page, int PageSize) : IRequest<Result<PagedResult<GenreDto>>>;

public class GetGenresListQueryCommandHandler : IRequestHandler<GetGenresListQueryCommand, Result<PagedResult<GenreDto>>> {
    private readonly IApplicationDbContext _context;

    public GetGenresListQueryCommandHandler(IApplicationDbContext context) {
        _context = context;
    }

    public async Task<Result<PagedResult<GenreDto>>> Handle(GetGenresListQueryCommand request, CancellationToken cancellationToken) {
        return new Result<PagedResult<GenreDto>>(await ResultExtensions.CreatePagedResultAsync(
            _context.Genres
                .OrderByDescending(e => e.LastModified)
                .ThenByDescending(e => e.CreateAt)
                .Select(e => new GenreDto(e)),
            request.Page,
            request.PageSize));
    }
}

public class GetGenresListQueryCommandValidator : AbstractValidator<GetGenresListQueryCommand> {

    public GetGenresListQueryCommandValidator() {
        RuleFor(e => e.Page).GreaterThanOrEqualTo(1);
        RuleFor(e => e.PageSize).GreaterThanOrEqualTo(1);
    }
}