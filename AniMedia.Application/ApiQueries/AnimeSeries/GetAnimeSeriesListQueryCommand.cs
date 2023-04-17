using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AniMedia.Application.ApiQueries.AnimeSeries; 

public record GetAnimeSeriesListQueryCommand : IRequest<Result<List<AnimeSeriesDto>>>;

public class GetAnimeSeriesListQueryCommandHandler : IRequestHandler<GetAnimeSeriesListQueryCommand, Result<List<AnimeSeriesDto>>> {

    private readonly IApplicationDbContext _context;

    public GetAnimeSeriesListQueryCommandHandler(IApplicationDbContext context) {
        _context = context;
    }

    public async Task<Result<List<AnimeSeriesDto>>> Handle(GetAnimeSeriesListQueryCommand request, CancellationToken cancellationToken) {
        var animeSeries = await _context.AnimeSeries
            .Select(e => new AnimeSeriesDto(e))
            .ToListAsync(cancellationToken);

        return new Result<List<AnimeSeriesDto>>(animeSeries);
    }
}