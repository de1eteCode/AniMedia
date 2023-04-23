using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AniMedia.Application.ApiQueries.Genres; 

/// <summary>
/// Получение жанра
/// </summary>
/// <param name="Page">Страница</param>
public record GetGenreQueryCommand(Guid GenreUid) : IRequest<Result<GenreDto>>;

public class GetGenreQueryCommandHandler : IRequestHandler<GetGenreQueryCommand, Result<GenreDto>> {
    private readonly IApplicationDbContext _context;

    public GetGenreQueryCommandHandler(IApplicationDbContext context) {
        _context = context;
    }

    public async Task<Result<GenreDto>> Handle(GetGenreQueryCommand request, CancellationToken cancellationToken) {
        var genre = await _context.Genres.SingleOrDefaultAsync(
            e => e.UID.Equals(request.GenreUid),
            cancellationToken);

        if (genre == null) {
            return new Result<GenreDto>(new EntityNotFoundError());
        }

        return new Result<GenreDto>(new GenreDto(genre));
    }
}

public class GetGenreQueryCommandValidator : AbstractValidator<GetGenreQueryCommand> {

    public GetGenreQueryCommandValidator() {
        RuleFor(e => e.GenreUid).NotEmpty();
    }
}