using AniMedia.Application.Common.Attributes;
using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using FluentValidation;
using MediatR;

namespace AniMedia.Application.ApiQueries.RateAnimeSeries;

[ApplicationAuthorize]
public record GetRateAnimeSeriesListQueryCommand(int Page, int PageSize) : IRequest<PagedResult<RateAnimeSeriesDto>>;

public class GetRateAnimeSeriesListQueryCommandHandler : IRequestHandler<GetRateAnimeSeriesListQueryCommand, PagedResult<RateAnimeSeriesDto>> {
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetRateAnimeSeriesListQueryCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService) {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<PagedResult<RateAnimeSeriesDto>> Handle(GetRateAnimeSeriesListQueryCommand request, CancellationToken cancellationToken) {
        return await ResultExtensions.CreatePagedResultAsync(
            _context.Rates
                .Where(e => e.UserUid.Equals(_currentUserService.UserUID))
                .OrderByDescending(e => e.LastModified)
                .ThenByDescending(e => e.CreateAt)
                .Select(e => new RateAnimeSeriesDto(e)),
            request.Page,
            request.PageSize);
    }
}

public class GetRateAnimeSeriesListQueryCommandValidator : AbstractValidator<GetRateAnimeSeriesListQueryCommand> {

    public GetRateAnimeSeriesListQueryCommandValidator() {
        RuleFor(e => e.Page).GreaterThanOrEqualTo(1);
        RuleFor(e => e.PageSize).GreaterThanOrEqualTo(1);
    }
}