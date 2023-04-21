using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Entities;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AniMedia.Application.ApiCommands.RateAnimeSeries; 

/// <summary>
/// Установка рейтинга для пользователя
/// </summary>
/// <param name="UserUid">Идентификатор пользователя</param>
/// <param name="AnimeSeriesUid">Идентификатор аниме серии</param>
/// <param name="Rate">Рейтинг</param>
public record RateAnimeSeriesCommand(Guid UserUid, Guid AnimeSeriesUid, byte Rate) : IRequest<Result<RateAnimeSeriesDto>>;

public class RateAnimeSeriesCommandHandler : IRequestHandler<RateAnimeSeriesCommand, Result<RateAnimeSeriesDto>> {

    private readonly IApplicationDbContext _context;

    public RateAnimeSeriesCommandHandler(IApplicationDbContext context) {
        _context = context;
    }

    public async Task<Result<RateAnimeSeriesDto>> Handle(RateAnimeSeriesCommand request, CancellationToken cancellationToken) {
        // find rate
        var currentRate = await _context.Rates
            .SingleOrDefaultAsync(e => 
                e.AnimeSeriesUid.Equals(request.AnimeSeriesUid) &&
                e.UserUid.Equals(request.UserUid));
        
        // if null - create, else - update
        if (currentRate == null) {
            currentRate = new RateAnimeSeriesEntity {
                AnimeSeriesUid = request.AnimeSeriesUid,
                UserUid = request.UserUid,
                Rate = request.Rate
            };

            await _context.Rates.AddAsync(currentRate, cancellationToken);
        }
        else {
            currentRate.Rate = request.Rate;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return new Result<RateAnimeSeriesDto>(new RateAnimeSeriesDto(currentRate));
    }
}

public class RateAnimeSeriesCommandValidator : AbstractValidator<RateAnimeSeriesCommand> {

    public RateAnimeSeriesCommandValidator() {
        RuleFor(e => e.UserUid).NotEmpty();
        RuleFor(e => e.AnimeSeriesUid).NotEmpty();
        RuleFor(e => e.Rate).InclusiveBetween((byte)1, (byte)10);
    }
}