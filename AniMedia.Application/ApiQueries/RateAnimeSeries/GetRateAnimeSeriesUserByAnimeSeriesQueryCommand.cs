using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AniMedia.Application.ApiQueries.RateAnimeSeries; 

/// <summary>
/// Команда получения оценки аниме пользователя
/// </summary>
/// <param name="UserUid">Идентификатор пользователя</param>
/// <param name="AnimeSeriesUid">Идентификатор аниме сериала</param>
public record GetRateAnimeSeriesUserByAnimeSeriesQueryCommand(Guid UserUid, Guid AnimeSeriesUid) : 
    IRequest<Result<RateAnimeSeriesDto>>;

public class GetRateAnimeSeriesByAnimeSeriesQueryCommandHandler :
    IRequestHandler<GetRateAnimeSeriesUserByAnimeSeriesQueryCommand, Result<RateAnimeSeriesDto>> {

    private readonly IApplicationDbContext _context;

    public GetRateAnimeSeriesByAnimeSeriesQueryCommandHandler(IApplicationDbContext context) {
        _context = context;
    }

    public async Task<Result<RateAnimeSeriesDto>> Handle(
        GetRateAnimeSeriesUserByAnimeSeriesQueryCommand request, 
        CancellationToken cancellationToken) {

        var rate = await _context.Rates
            .SingleOrDefaultAsync(e =>
                    e.AnimeSeriesUid.Equals(request.AnimeSeriesUid) &&
                    e.UserUid.Equals(request.UserUid),
                cancellationToken);

        if (rate == null) {
            return new Result<RateAnimeSeriesDto>(new EntityNotFoundError());
        }

        return new Result<RateAnimeSeriesDto>(new RateAnimeSeriesDto(rate));
    }
}

public class GetRateAnimeSeriesByAnimeSeriesQueryCommandValidator :
    AbstractValidator<GetRateAnimeSeriesUserByAnimeSeriesQueryCommand> {

    public GetRateAnimeSeriesByAnimeSeriesQueryCommandValidator() {
        RuleFor(e => e.UserUid).NotEmpty();
        RuleFor(e => e.AnimeSeriesUid).NotEmpty();
    }
}