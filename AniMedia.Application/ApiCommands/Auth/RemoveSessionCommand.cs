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
public record RemoveSessionCommand(Guid UserUid, Guid SessionUid) : IRequest<Result<SessionDto>>;

public class RemoveSessionCommandHandler : IRequestHandler<RemoveSessionCommand, Result<SessionDto>> {
    private readonly IApplicationDbContext _context;

    public RemoveSessionCommandHandler(IApplicationDbContext context) {
        _context = context;
    }

    public async Task<Result<SessionDto>> Handle(RemoveSessionCommand request, CancellationToken cancellationToken) {
        var session = await _context.Sessions.FirstOrDefaultAsync(e =>
            e.UserUid.Equals(request.UserUid) && e.UID.Equals(request.SessionUid),
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
        RuleFor(x => x.UserUid).NotEmpty();
        RuleFor(x => x.SessionUid).NotEmpty();
    }
}