using AniMedia.Application.Common.Attributes;
using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using FluentValidation;
using MediatR;

namespace AniMedia.Application.ApiCommands.AnimeSeries;

[ApplicationAuthorize]
public record UpdateAnimeSeriesCommand : IRequest<Result<AnimeSeriesDto>> {
    public Guid Uid { get; init; }
    public required string Name { get; init; }
    public required string JapaneseName { get; init; }
    public required string Description { get; init; }
    public DateTime? DateOfRelease { get; init; }
    public DateTime? DateOfAnnouncement { get; init; }
    public int? ExistTotalEpisodes { get; init; }
    public int? PlanedTotalEpisodes { get; init; }

    public ICollection<GenreDto> Genres { get; init; } = new List<GenreDto>();
}

public class UpdateAnimeSeriesCommandHandler : IRequestHandler<UpdateAnimeSeriesCommand, Result<AnimeSeriesDto>> {
    private readonly IApplicationDbContext _context;

    public UpdateAnimeSeriesCommandHandler(IApplicationDbContext context) {
        _context = context;
    }

    public Task<Result<AnimeSeriesDto>> Handle(UpdateAnimeSeriesCommand request, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }
}

public class UpdateAnimeSeriesCommandValidator : AbstractValidator<UpdateAnimeSeriesCommand> {

    public UpdateAnimeSeriesCommandValidator() {
        RuleFor(e => e.Uid).NotEmpty();
        RuleFor(e => e.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(512);
    }
}