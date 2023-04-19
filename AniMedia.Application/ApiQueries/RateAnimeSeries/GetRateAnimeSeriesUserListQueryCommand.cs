using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using FluentValidation;
using MediatR;

namespace AniMedia.Application.ApiQueries.RateAnimeSeries;

/// <summary>
/// Команда получения списка оценок аниме сериалов пользователя
/// </summary>
/// <param name="UserUid">Идентификатор пользователя</param>
/// <param name="Page">Страница</param>
/// <param name="PageSize">Размер страницы</param>
public record GetRateAnimeSeriesUserListQueryCommand(Guid UserUid, int Page, int PageSize) : IRequest<PagedResult<RateAnimeSeriesDto>>;

public class GetRateAnimeSeriesListQueryCommandHandler : IRequestHandler<GetRateAnimeSeriesUserListQueryCommand, PagedResult<RateAnimeSeriesDto>> {
    private readonly IApplicationDbContext _context;

    public GetRateAnimeSeriesListQueryCommandHandler(IApplicationDbContext context) {
        _context = context;
    }

    public async Task<PagedResult<RateAnimeSeriesDto>> Handle(GetRateAnimeSeriesUserListQueryCommand request, CancellationToken cancellationToken) {
        return await ResultExtensions.CreatePagedResultAsync(
            _context.Rates
                .Where(e => e.UserUid.Equals(request.UserUid))
                .OrderByDescending(e => e.LastModified)
                .ThenByDescending(e => e.CreateAt)
                .Select(e => new RateAnimeSeriesDto(e)),
            request.Page,
            request.PageSize);
    }
}

public class GetRateAnimeSeriesListQueryCommandValidator : AbstractValidator<GetRateAnimeSeriesUserListQueryCommand> {

    public GetRateAnimeSeriesListQueryCommandValidator() {
        RuleFor(e => e.UserUid).NotEmpty();
        RuleFor(e => e.Page).GreaterThanOrEqualTo(1);
        RuleFor(e => e.PageSize).GreaterThanOrEqualTo(1);
    }
}