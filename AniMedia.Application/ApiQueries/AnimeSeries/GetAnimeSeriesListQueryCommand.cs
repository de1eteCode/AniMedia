using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using FluentValidation;
using MediatR;

namespace AniMedia.Application.ApiQueries.AnimeSeries;

public record GetAnimeSeriesListQueryCommand(int Page, int PageSize) : IRequest<PagedResult<AnimeSeriesDto>>;

public class GetAnimeSeriesListQueryCommandHandler : IRequestHandler<GetAnimeSeriesListQueryCommand, PagedResult<AnimeSeriesDto>> {
    private readonly IApplicationDbContext _context;

    public GetAnimeSeriesListQueryCommandHandler(IApplicationDbContext context) {
        _context = context;
    }

    public async Task<PagedResult<AnimeSeriesDto>> Handle(GetAnimeSeriesListQueryCommand request, CancellationToken cancellationToken) {
        return await ResultExtensions.CreatePagedResultAsync(
            _context.AnimeSeries
                .OrderByDescending(e => e.LastModified)
                .ThenByDescending(e => e.CreateAt)
                .Select(e => new AnimeSeriesDto(e)),
            request.Page,
            request.PageSize);
    }
}

public class GetAnimeSeriesListQueryCommandValidator : AbstractValidator<GetAnimeSeriesListQueryCommand> {

    public GetAnimeSeriesListQueryCommandValidator() {
        RuleFor(e => e.Page).GreaterThanOrEqualTo(1);
        RuleFor(e => e.PageSize).GreaterThanOrEqualTo(1);
    }
}