using AniMedia.Application.Common.Attributes;
using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Entities;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AniMedia.Application.ApiCommands.AnimeSeries;

[ApplicationAuthorize]
public record UpdateAnimeSeriesCommand : IRequest<Result<AnimeSeriesDto>> {
    public Guid Uid { get; init; }
    public required string Name { get; init; }
    public required string EnglishName { get; init; }
    public required string JapaneseName { get; init; }
    public required string Description { get; init; }
    public DateTime? DateOfRelease { get; init; }
    public DateTime? DateOfAnnouncement { get; init; }
    public int? ExistTotalEpisodes { get; init; }
    public int? PlanedTotalEpisodes { get; init; }

    public required IEnumerable<GenreDto> Genres { get; init; }
}

public class UpdateAnimeSeriesCommandHandler : IRequestHandler<UpdateAnimeSeriesCommand, Result<AnimeSeriesDto>> {
    private readonly IApplicationDbContext _context;

    public UpdateAnimeSeriesCommandHandler(IApplicationDbContext context) {
        _context = context;
    }

    public async Task<Result<AnimeSeriesDto>> Handle(UpdateAnimeSeriesCommand request, CancellationToken cancellationToken) {
        var animeSeries = await _context.AnimeSeries
            .SingleOrDefaultAsync(e => e.UID.Equals(request.Uid), cancellationToken);

        if (animeSeries == null) {
            return new Result<AnimeSeriesDto>(new EntityNotFoundError());
        }

        animeSeries.Name = request.Name;
        animeSeries.EnglishName = request.EnglishName;
        animeSeries.JapaneseName = request.JapaneseName;
        animeSeries.Description = request.Description;
        animeSeries.DateOfRelease = request.DateOfRelease;
        animeSeries.DateOfAnnouncement = request.DateOfAnnouncement;
        animeSeries.ExistTotalEpisodes = request.ExistTotalEpisodes;
        animeSeries.PlanedTotalEpisodes = request.PlanedTotalEpisodes;

        var genreDtos = request.Genres.ToList();

        // remove genere if not contains in genredtos
        if (animeSeries.Genres.Any()) {
            foreach (var genreToRemove in animeSeries.Genres
                .Where(e => genreDtos.Any(s => s.Uid.Equals(e.GenreUid)) == false)
                .ToList()) {
                animeSeries.Genres.Remove(genreToRemove);
            }
        }

        // add genre if not contains in animeseries.genres
        var genresToAdd = new List<AnimeSeriesGenreEntity>();

        await foreach (var genre in _context.Genres.AsAsyncEnumerable()) {
            if (animeSeries.Genres.Any(e => e.GenreUid.Equals(genre.UID)) == false &&
                genreDtos.Any(e => e.Uid.Equals(genre.UID))) {
                genresToAdd.Add(new AnimeSeriesGenreEntity {
                    AnimeSeries = animeSeries,
                    Genre = genre
                });
            }
        }

        genresToAdd.ForEach(animeSeries.Genres.Add);

        await _context.SaveChangesAsync(cancellationToken);

        return new Result<AnimeSeriesDto>(new AnimeSeriesDto(animeSeries));
    }
}

public class UpdateAnimeSeriesCommandValidator : AbstractValidator<UpdateAnimeSeriesCommand> {

    public UpdateAnimeSeriesCommandValidator() {
        RuleFor(e => e.Uid).NotEmpty();

        RuleFor(e => e.Name)
            .NotEmpty()
            .Length(3, 512);

        RuleFor(e => e.EnglishName)
            .NotEmpty()
            .Length(3, 512);

        RuleFor(e => e.JapaneseName)
            .NotEmpty()
            .Length(3, 512);

        RuleFor(e => e.Genres).NotNull();

        RuleForEach(e => e.Genres).ChildRules(genre => {
            genre.RuleFor(e => e.Uid).NotEmpty();
        });
    }
}