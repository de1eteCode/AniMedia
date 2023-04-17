using AniMedia.Application.Common.Attributes;
using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AniMedia.Application.ApiQueries.Account;

[ApplicationAuthorize]
public record GetProfileQueryCommand : IRequest<Result<ProfileUserDto>>;

public class GetProfileQueryCommandHandler : IRequestHandler<GetProfileQueryCommand, Result<ProfileUserDto>> {
    private readonly ICurrentUserService _currentUser;
    private readonly IApplicationDbContext _context;

    public GetProfileQueryCommandHandler(ICurrentUserService currentUser, IApplicationDbContext context) {
        _currentUser = currentUser;
        _context = context;
    }

    public async Task<Result<ProfileUserDto>> Handle(GetProfileQueryCommand request, CancellationToken cancellationToken) {
        var user = await _context.Users
            .FirstOrDefaultAsync(e => e.UID.Equals(_currentUser.UserUID), cancellationToken);

        if (user == null) {
            return new Result<ProfileUserDto>(new EntityNotFoundError());
        }

        return new Result<ProfileUserDto>(new ProfileUserDto(user));
    }
}