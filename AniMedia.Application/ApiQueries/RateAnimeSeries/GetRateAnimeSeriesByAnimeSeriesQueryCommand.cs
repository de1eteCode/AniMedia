using AniMedia.Application.Common.Attributes;
using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AniMedia.Application.ApiQueries.RateAnimeSeries; 

[ApplicationAuthorize]
public record GetRateAnimeSeriesByAnimeSeriesQueryCommand(Guid AnimeSeriesUid) : 
    IRequest<Result<RateAnimeSeriesDto>>;

public class GetRateAnimeSeriesByAnimeSeriesQueryCommandHandler :
    IRequestHandler<GetRateAnimeSeriesByAnimeSeriesQueryCommand, Result<RateAnimeSeriesDto>> {

    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetRateAnimeSeriesByAnimeSeriesQueryCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUserService) {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result<RateAnimeSeriesDto>> Handle(
        GetRateAnimeSeriesByAnimeSeriesQueryCommand request, 
        CancellationToken cancellationToken) {

        var rate = await _context.Rates
            .SingleOrDefaultAsync(e =>
                    e.AnimeSeriesUid.Equals(request.AnimeSeriesUid) &&
                    e.UserUid.Equals(_currentUserService.UserUID),
                cancellationToken);

        if (rate == null) {
            return new Result<RateAnimeSeriesDto>(new EntityNotFoundError());
        }

        return new Result<RateAnimeSeriesDto>(new RateAnimeSeriesDto(rate));
    }
}

public class GetRateAnimeSeriesByAnimeSeriesQueryCommandValidator :
    AbstractValidator<GetRateAnimeSeriesByAnimeSeriesQueryCommand> {

    public GetRateAnimeSeriesByAnimeSeriesQueryCommandValidator() {
        RuleFor(e => e.AnimeSeriesUid).NotEmpty();
    }
}