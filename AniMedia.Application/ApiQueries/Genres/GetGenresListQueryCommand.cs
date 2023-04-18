using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using FluentValidation;
using MediatR;

namespace AniMedia.Application.ApiQueries.Genres;

public record GetGenresListQueryCommand(int Page, int PageSize) : IRequest<PagedResult<GenreDto>>;

public class GetGenresListQueryCommandHandler : IRequestHandler<GetGenresListQueryCommand, PagedResult<GenreDto>> {
    private readonly IApplicationDbContext _context;

    public GetGenresListQueryCommandHandler(IApplicationDbContext context) {
        _context = context;
    }

    public async Task<PagedResult<GenreDto>> Handle(GetGenresListQueryCommand request, CancellationToken cancellationToken) {
        return await ResultExtensions.CreatePagedResultAsync(
            _context.Genres
                .OrderByDescending(e => e.LastModified)
                .ThenByDescending(e => e.CreateAt)
                .Select(e => new GenreDto(e)),
            request.Page,
            request.PageSize);
    }
}

public class GetGenresListQueryCommandValidator : AbstractValidator<GetGenresListQueryCommand> {

    public GetGenresListQueryCommandValidator() {
        RuleFor(e => e.Page).GreaterThanOrEqualTo(1);
        RuleFor(e => e.PageSize).GreaterThanOrEqualTo(1);
    }
}