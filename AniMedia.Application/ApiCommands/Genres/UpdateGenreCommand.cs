using AniMedia.Application.Common.Attributes;
using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Models.Genres.Dtos;
using AniMedia.Domain.Models.Genres.Requests;
using AniMedia.Domain.Models.Responses;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AniMedia.Application.ApiCommands.Genres; 

/// <summary>
/// Команда обновления жанра
/// </summary>
/// <param name="Model">Запрос на обновление жанра</param>
[ApplicationAuthorize]
public record UpdateGenreCommand(UpdateGenreRequest Model) : IRequest<Result<GenreDto>>;

public class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand, Result<GenreDto>> {

    private readonly IApplicationDbContext _context;

    public UpdateGenreCommandHandler(IApplicationDbContext context) {
        _context = context;
    }
    
    public async Task<Result<GenreDto>> Handle(UpdateGenreCommand request, CancellationToken cancellationToken) {
        var genre = await _context.Genres.SingleOrDefaultAsync(e => e.UID.Equals(request.Model.Uid), cancellationToken);

        if (genre == null) {
            return new Result<GenreDto>(new EntityNotFoundError());
        }

        genre.Name = request.Model.Name;

        await _context.SaveChangesAsync(cancellationToken);

        return new Result<GenreDto>(new GenreDto(genre));
    }
}

public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand> {

    public UpdateGenreCommandValidator() {
        RuleFor(e => e.Model).NotNull();
        RuleFor(e => e.Model.Uid).NotEmpty();
        RuleFor(e => e.Model.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(64);
    }
}