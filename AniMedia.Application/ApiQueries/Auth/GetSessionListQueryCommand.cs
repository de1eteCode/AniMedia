using AniMedia.Application.Common.Attributes;
using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AniMedia.Application.ApiQueries.Auth;

[ApplicationAuthorize]
public record GetSessionListQueryCommand : IRequest<Result<List<SessionDto>>>;

public class GetSessionListQueryCommandHandler : IRequestHandler<GetSessionListQueryCommand, Result<List<SessionDto>>> {
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetSessionListQueryCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService) {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result<List<SessionDto>>> Handle(GetSessionListQueryCommand request, CancellationToken cancellationToken) {
        var sessions = await _context.Sessions
            .Where(e => e.UserUid.Equals(_currentUserService.UserUID))
            .Select(e => new SessionDto(e))
            .ToListAsync(cancellationToken);

        return new Result<List<SessionDto>>(sessions);
    }
}