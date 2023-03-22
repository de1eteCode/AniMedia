using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AniMedia.Application.ApiCommands.Auth;
public record RemoveSessionCommand(Guid SessionUid) : IRequest<Result<SessionDto>>;

public class RemoveSessionCommandHandler : IRequestHandler<RemoveSessionCommand, Result<SessionDto>> {
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public RemoveSessionCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService) {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result<SessionDto>> Handle(RemoveSessionCommand request, CancellationToken cancellationToken) {
        if (_currentUserService.UserUID == null) {
            return new Result<SessionDto>(new AuthenticationError("Not auth user"));
        }

        var session = await _context.Sessions.FirstOrDefaultAsync(e => e.UserUid.Equals(_currentUserService.UserUID) && e.UID.Equals(request.SessionUid), cancellationToken);

        if (session == null) {
            return new Result<SessionDto>(new EntityNotFoundError());
        }

        _context.Sessions.Remove(session);
        await _context.SaveChangesAsync(cancellationToken);

        return new Result<SessionDto>(new SessionDto(session));
    }
}