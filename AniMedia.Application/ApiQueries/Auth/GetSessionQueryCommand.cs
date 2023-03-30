using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AniMedia.Application.ApiQueries.Auth;

public record GetSessionQueryCommand(string AccessToken) : IRequest<Result<SessionDto>>;

public class GetSessionQueryCommandHandler : IRequestHandler<GetSessionQueryCommand, Result<SessionDto>> {
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetSessionQueryCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService) {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result<SessionDto>> Handle(GetSessionQueryCommand request, CancellationToken cancellationToken) {
        if (_currentUserService.UserUID == null)
            return new Result<SessionDto>(new AuthenticationError("Not auth user"));

        var session = await _context.Sessions
            .FirstOrDefaultAsync(
                e => e.AccessToken.Equals(request.AccessToken) && e.UserUid.Equals(_currentUserService.UserUID),
                cancellationToken);

        if (session == null) return new Result<SessionDto>(new EntityNotFoundError("Session not found"));

        return new Result<SessionDto>(new SessionDto(session));
    }
}