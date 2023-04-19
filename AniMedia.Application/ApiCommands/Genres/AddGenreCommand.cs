using AniMedia.Application.Common.Attributes;
using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Entities;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using FluentValidation;
using MediatR;

namespace AniMedia.Application.ApiCommands.Genres;

/// <summary>
/// Добавление жанра аниме сериала
/// </summary>
/// <param name="Name">Наименование жанра</param>
[ApplicationAuthorize]
public record AddGenreCommand(string Name) : IRequest<Result<GenreDto>>;

public class AddGenreCommandHandler : IRequestHandler<AddGenreCommand, Result<GenreDto>> {
    private readonly IApplicationDbContext _context;

    public AddGenreCommandHandler(IApplicationDbContext context) {
        _context = context;
    }

    public async Task<Result<GenreDto>> Handle(AddGenreCommand request, CancellationToken cancellationToken) {
        var genre = new GenreEntity(request.Name);

        await _context.Genres.AddAsync(genre, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return new Result<GenreDto>(new GenreDto(genre));
    }
}

public class AddGenreCommandValidator : AbstractValidator<AddGenreCommand> {

    public AddGenreCommandValidator() {
        RuleFor(e => e.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(64);
    }
}