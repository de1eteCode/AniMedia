using AniMedia.Application.Common.Attributes;
using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Constants;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AniMedia.Application.ApiCommands.Account;

[ApplicationAuthorize]
public record UpdateProfileCommand(string FirstName, string SecondName) : IRequest<Result<ProfileUserDto>>;

public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, Result<ProfileUserDto>> {
    private readonly ICurrentUserService _currentUser;
    private readonly IApplicationDbContext _context;

    public UpdateProfileCommandHandler(ICurrentUserService currentUser, IApplicationDbContext context) {
        _currentUser = currentUser;
        _context = context;
    }

    public async Task<Result<ProfileUserDto>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken) {
        var user = await _context.Users
            .FirstOrDefaultAsync(e => e.UID.Equals(_currentUser.UserUID), cancellationToken);

        if (user == null) {
            return new Result<ProfileUserDto>(new EntityNotFoundError());
        }

        user.FirstName = request.FirstName;
        user.SecondName = request.SecondName;

        await _context.SaveChangesAsync(cancellationToken);

        return new Result<ProfileUserDto>(new ProfileUserDto(user));
    }
}