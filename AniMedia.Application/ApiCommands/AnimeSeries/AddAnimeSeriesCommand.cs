using AniMedia.Application.Common.Attributes;
using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Entities;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using FluentValidation;
using MediatR;

namespace AniMedia.Application.ApiCommands.AnimeSeries;

[ApplicationAuthorize]
public record AddAnimeSeriesCommand : IRequest<Result<AnimeSeriesDto>> {
    public required string Name { get; init; }
    public required string EnglishName { get; init; }
    public required string JapaneseName { get; init; }
    public required string Description { get; init; }
    public DateTime? DateOfRelease { get; init; }
    public DateTime? DateOfAnnouncement { get; init; }
    public int? ExistTotalEpisodes { get; init; }
    public int? PlanedTotalEpisodes { get; init; }

    public required IEnumerable<GenreDto> Genres { get; init; }
};

public class AddAnimeSeriesCommandHandler : IRequestHandler<AddAnimeSeriesCommand, Result<AnimeSeriesDto>> {
    private readonly IApplicationDbContext _context;

    public AddAnimeSeriesCommandHandler(IApplicationDbContext context) {
        _context = context;
    }

    public async Task<Result<AnimeSeriesDto>> Handle(AddAnimeSeriesCommand request, CancellationToken cancellationToken) {
        var animeSeries = new AnimeSeriesEntity {
            Name = request.Name,
            EnglishName = request.EnglishName,
            JapaneseName = request.JapaneseName,
            Description = request.Description,
            DateOfRelease = request.DateOfRelease,
            DateOfAnnouncement = request.DateOfAnnouncement,
            ExistTotalEpisodes = request.ExistTotalEpisodes,
            PlanedTotalEpisodes = request.PlanedTotalEpisodes,
        };

        var trackedEntity = await _context.AnimeSeries.AddAsync(animeSeries);

        var genreDtos = request.Genres.ToList();

        var genreUids = _context.Genres
            .AsAsyncEnumerable()
            .WithCancellation(cancellationToken);

        await foreach (var genre in genreUids) {
            if (genreDtos.Any(s => s.Uid.Equals(genre.UID))) {
                trackedEntity.Entity.Genres.Add(new AnimeSeriesGenreEntity() {
                    Genre = genre,
                    AnimeSeries = trackedEntity.Entity
                });
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        return new Result<AnimeSeriesDto>(new AnimeSeriesDto(trackedEntity.Entity));
    }
}

public class AddAnimeSeriesCommandValidator : AbstractValidator<AddAnimeSeriesCommand> {

    public AddAnimeSeriesCommandValidator() {
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