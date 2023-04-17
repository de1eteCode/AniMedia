using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AniMedia.Application.ApiQueries.Genres; 

public record GetGenresListQueryCommand : IRequest<Result<List<GenreDto>>>;

public class GetGenresListQueryCommandHandler : IRequestHandler<GetGenresListQueryCommand, Result<List<GenreDto>>> {

    private readonly IApplicationDbContext _context;

    public GetGenresListQueryCommandHandler(IApplicationDbContext context) {
        _context = context;
    }

    public async Task<Result<List<GenreDto>>> Handle(GetGenresListQueryCommand request, CancellationToken cancellationToken) {
        var genres = await _context.Genres
            .Select(e => new GenreDto(e))
            .ToListAsync(cancellationToken);

        return new Result<List<GenreDto>>(genres);
    }
}