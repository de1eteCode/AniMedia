using AniMedia.Application.Common.Attributes;
using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Models.AnimeSeries.Dtos;
using AniMedia.Domain.Models.AnimeSeries.Requests;
using AniMedia.Domain.Models.Responses;
using FluentValidation;
using MediatR;

namespace AniMedia.Application.ApiCommands.AnimeSeries; 

[ApplicationAuthorize]
public record UpdateAnimeSeriesCommand(UpdateAnimeSeriesRequest Model) : IRequest<Result<AnimeSeriesDto>>;

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
        RuleFor(e => e.Model).NotNull();
        RuleFor(e => e.Model.Uid).NotEmpty();
        RuleFor(e => e.Model.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(512);
    }
}