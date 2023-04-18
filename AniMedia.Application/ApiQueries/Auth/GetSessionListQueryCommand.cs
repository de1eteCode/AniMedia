using AniMedia.Application.Common.Attributes;
using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Models.Responses;
using AniMedia.Domain.Models.Dtos;
using MediatR;
using FluentValidation;

namespace AniMedia.Application.ApiQueries.Auth;

[ApplicationAuthorize]
public record GetSessionListQueryCommand(int Page, int PageSize) : IRequest<PagedResult<SessionDto>>;

public class GetSessionListQueryCommandHandler : IRequestHandler<GetSessionListQueryCommand, PagedResult<SessionDto>> {
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetSessionListQueryCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService) {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<PagedResult<SessionDto>> Handle(GetSessionListQueryCommand request, CancellationToken cancellationToken) {
        return await ResultExtensions.CreatePagedResultAsync(
            _context.Sessions
                .Where(e => e.UserUid.Equals(_currentUserService.UserUID))
                .OrderByDescending(e => e.CreateAt)
                .Select(e => new SessionDto(e)),
            request.Page,
            request.PageSize);
    }
}

public class GetSessionListQueryCommandValidator : AbstractValidator<GetSessionListQueryCommand> {

    public GetSessionListQueryCommandValidator() {
        RuleFor(e => e.Page).GreaterThanOrEqualTo(1);
        RuleFor(e => e.PageSize).GreaterThanOrEqualTo(1);
    }
}