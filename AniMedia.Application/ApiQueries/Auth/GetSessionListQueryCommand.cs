using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Models.Responses;
using AniMedia.Domain.Models.Dtos;
using MediatR;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AniMedia.Application.ApiQueries.Auth;

/// <summary>
/// Получение списка сессий пользователя
/// </summary>
/// <param name="UserUid">Идентификатор пользователя</param>
/// <param name="Page">Страница</param>
/// <param name="PageSize">Размер страницы</param>
public record GetSessionListQueryCommand(Guid UserUid, int Page, int PageSize) : IRequest<Result<PagedResult<SessionDto>>>;

public class GetSessionListQueryCommandHandler : IRequestHandler<GetSessionListQueryCommand, Result<PagedResult<SessionDto>>> {
    private readonly IApplicationDbContext _context;

    public GetSessionListQueryCommandHandler(IApplicationDbContext context) {
        _context = context;
    }

    public async Task<Result<PagedResult<SessionDto>>> Handle(GetSessionListQueryCommand request, CancellationToken cancellationToken) {
        var user = await _context.Users.SingleOrDefaultAsync(e => 
            e.UID.Equals(request.UserUid), 
            cancellationToken);

        if (user == null) {
            return new Result<PagedResult<SessionDto>>(new EntityNotFoundError());
        }

        //return new Result<IEnumerable<SessionDto>>(user.Sessions.Select(e => new SessionDto(e)));

        return new Result<PagedResult<SessionDto>>(await ResultExtensions.CreatePagedResultAsync(
            user.Sessions
                .OrderByDescending(e => e.CreateAt)
                .Select(e => new SessionDto(e)),
            request.Page,
            request.PageSize));
    }
}

public class GetSessionListQueryCommandValidator : AbstractValidator<GetSessionListQueryCommand> {

    public GetSessionListQueryCommandValidator() {
        RuleFor(e => e.UserUid).NotEmpty();
        RuleFor(e => e.Page).GreaterThanOrEqualTo(1);
        RuleFor(e => e.PageSize).GreaterThanOrEqualTo(1);
    }
}