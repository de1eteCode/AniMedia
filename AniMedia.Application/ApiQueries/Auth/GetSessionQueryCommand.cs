using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Models.Responses;
using AniMedia.Domain.Models.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

namespace AniMedia.Application.ApiQueries.Auth;

/// <summary>
/// Получение сессии по токену
/// </summary>
/// <param name="AccessToken">Токен доступа</param>
public record GetSessionQueryCommand(Guid UserUid, string AccessToken) : IRequest<Result<SessionDto>>;

public class GetSessionQueryCommandHandler : IRequestHandler<GetSessionQueryCommand, Result<SessionDto>> {
    private readonly IApplicationDbContext _context;

    public GetSessionQueryCommandHandler(IApplicationDbContext context) {
        _context = context;
    }

    public async Task<Result<SessionDto>> Handle(GetSessionQueryCommand request, CancellationToken cancellationToken) {
        var session = await _context.Sessions
            .FirstOrDefaultAsync(e => e.AccessToken.Equals(request.AccessToken) && e.UserUid.Equals(request.UserUid), cancellationToken);

        if (session == null) {
            return new Result<SessionDto>(new EntityNotFoundError());
        }

        return new Result<SessionDto>(new SessionDto(session));
    }
}

public class GetSessionQueryCommandValidator : AbstractValidator<GetSessionQueryCommand> {

    public GetSessionQueryCommandValidator() {
        RuleFor(e => e.UserUid).NotEmpty();
        RuleFor(e => e.AccessToken).NotEmpty();
    }
}