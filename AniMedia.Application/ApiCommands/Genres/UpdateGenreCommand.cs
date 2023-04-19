using AniMedia.Application.Common.Attributes;
using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AniMedia.Application.ApiCommands.Genres;

/// <summary>
/// Команда обновления жанра
/// </summary>
/// <param name="Uid">Идентификатор жанра</param>
/// <param name="Name">Новое наименование</param>
[ApplicationAuthorize]
public record UpdateGenreCommand(Guid Uid, string Name) : IRequest<Result<GenreDto>>;

public class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand, Result<GenreDto>> {
    private readonly IApplicationDbContext _context;

    public UpdateGenreCommandHandler(IApplicationDbContext context) {
        _context = context;
    }

    public async Task<Result<GenreDto>> Handle(UpdateGenreCommand request, CancellationToken cancellationToken) {
        var genre = await _context.Genres.SingleOrDefaultAsync(e => e.UID.Equals(request.Uid), cancellationToken);

        if (genre == null) {
            return new Result<GenreDto>(new EntityNotFoundError());
        }

        genre.Name = request.Name;

        await _context.SaveChangesAsync(cancellationToken);

        return new Result<GenreDto>(new GenreDto(genre));
    }
}

public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand> {

    public UpdateGenreCommandValidator() {
        RuleFor(e => e.Uid).NotEmpty();
        RuleFor(e => e.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(64);
    }
}