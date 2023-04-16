using AniMedia.Application.Common.Attributes;
using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Models.AnimeSeries.Dtos;
using AniMedia.Domain.Models.AnimeSeries.Requests;
using AniMedia.Domain.Models.Responses;
using FluentValidation;
using MediatR;

namespace AniMedia.Application.ApiCommands.AnimeSeries; 

[ApplicationAuthorize]
public record AddAnimeSeriesCommand(AddAnimeSeriesRequest Model) : IRequest<Result<AnimeSeriesDto>>;

public class AddAnimeSeriesCommandHandler : IRequestHandler<AddAnimeSeriesCommand, Result<AnimeSeriesDto>> {

    private readonly IApplicationDbContext _context;

    public AddAnimeSeriesCommandHandler(IApplicationDbContext context) {
        _context = context;
    }

    public Task<Result<AnimeSeriesDto>> Handle(AddAnimeSeriesCommand request, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }
}

public class AddAnimeSeriesCommandValidator : AbstractValidator<AddAnimeSeriesCommand> {

    public AddAnimeSeriesCommandValidator() {
        RuleFor(e => e.Model).NotNull();
        RuleFor(e => e.Model.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(512);
    }
}