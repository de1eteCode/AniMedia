using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AniMedia.Application.ApiQueries.AnimeSeries; 

public record GetAnimeSeriesQueryCommand(Guid AnimeSeriesUid) : IRequest<Result<AnimeSeriesDto>>;

public class GetAnimeSeriesQueryCommandHandler : IRequestHandler<GetAnimeSeriesQueryCommand, Result<AnimeSeriesDto>> {

    private readonly IApplicationDbContext _context;

    public GetAnimeSeriesQueryCommandHandler(IApplicationDbContext context) {
        _context = context;
    }

    public async Task<Result<AnimeSeriesDto>> Handle(GetAnimeSeriesQueryCommand request, CancellationToken cancellationToken) {
        var animeSeries = await _context.AnimeSeries
            .SingleOrDefaultAsync(e => e.UID.Equals(request.AnimeSeriesUid), cancellationToken);

        if (animeSeries == null) {
            return new Result<AnimeSeriesDto>(new EntityNotFoundError());
        }

        return new Result<AnimeSeriesDto>(new AnimeSeriesDto(animeSeries));
    }
}

public class GetAnimeSeriesQueryCommandValidator : AbstractValidator<GetAnimeSeriesQueryCommand> {
    
    public GetAnimeSeriesQueryCommandValidator() {
        RuleFor(e => e.AnimeSeriesUid).NotEmpty();
    }
}