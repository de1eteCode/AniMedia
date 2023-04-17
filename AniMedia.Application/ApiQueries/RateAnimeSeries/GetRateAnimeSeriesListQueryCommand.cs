using AniMedia.Application.Common.Attributes;
using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AniMedia.Application.ApiQueries.RateAnimeSeries; 

[ApplicationAuthorize]
public record GetRateAnimeSeriesListQueryCommand : IRequest<Result<List<RateAnimeSeriesDto>>>;

public class GetRateAnimeSeriesListQueryCommandHandler : IRequestHandler<GetRateAnimeSeriesListQueryCommand, Result<List<RateAnimeSeriesDto>>> {

    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetRateAnimeSeriesListQueryCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService) {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result<List<RateAnimeSeriesDto>>> Handle(GetRateAnimeSeriesListQueryCommand request, CancellationToken cancellationToken) {
        var rates = await _context.Rates
            .Where(e => e.UserUid.Equals(_currentUserService.UserUID))
            .Select(e => new RateAnimeSeriesDto(e))
            .ToListAsync(cancellationToken);

        return new Result<List<RateAnimeSeriesDto>>(rates);
    }
}