using AniMedia.Application.Common.Attributes;
using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Models.Responses;
using AniMedia.Domain.Models.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

namespace AniMedia.Application.ApiCommands.Auth;

/// <summary>
/// Удаление сессии
/// </summary>
/// <param name="SessionUid">Идентификатор сессии</param>
[ApplicationAuthorize]
public record RemoveSessionCommand(Guid SessionUid) : IRequest<Result<SessionDto>>;

public class RemoveSessionCommandHandler : IRequestHandler<RemoveSessionCommand, Result<SessionDto>> {
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public RemoveSessionCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService) {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result<SessionDto>> Handle(RemoveSessionCommand request, CancellationToken cancellationToken) {
        var session = await _context.Sessions.FirstOrDefaultAsync(e =>
            e.UserUid.Equals(_currentUserService.UserUID) && e.UID.Equals(request.SessionUid),
            cancellationToken);

        if (session == null) {
            return new Result<SessionDto>(new EntityNotFoundError());
        }

        _context.Sessions.Remove(session);
        await _context.SaveChangesAsync(cancellationToken);

        return new Result<SessionDto>(new SessionDto(session));
    }
}

public class RemoveSessionCommandValidator : AbstractValidator<RemoveSessionCommand> {

    public RemoveSessionCommandValidator() {
        RuleFor(x => x.SessionUid).NotEmpty();
    }
}