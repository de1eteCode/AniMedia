using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AniMedia.Application.ApiCommands.Account;

/// <summary>
/// Обновление профиля пользователя
/// </summary>
/// <param name="UserUid">Идентификатор пользователя</param>
/// <param name="FirstName">Имя</param>
/// <param name="SecondName">Фамилия</param>
public record UpdateProfileCommand(Guid UserUid, string FirstName, string SecondName) : IRequest<Result<ProfileUserDto>>;

public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, Result<ProfileUserDto>> {
    private readonly IApplicationDbContext _context;

    public UpdateProfileCommandHandler(IApplicationDbContext context) {
        _context = context;
    }

    public async Task<Result<ProfileUserDto>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken) {
        var user = await _context.Users
            .FirstOrDefaultAsync(e => e.UID.Equals(request.UserUid), cancellationToken);

        if (user == null) {
            return new Result<ProfileUserDto>(new EntityNotFoundError());
        }

        user.FirstName = request.FirstName;
        user.SecondName = request.SecondName;

        await _context.SaveChangesAsync(cancellationToken);

        return new Result<ProfileUserDto>(new ProfileUserDto(user));
    }
}

public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand> {

    public UpdateProfileCommandValidator() {
        RuleFor(e => e.UserUid).NotEmpty();
        RuleFor(e => e.FirstName).NotEmpty();
        RuleFor(e => e.SecondName).NotEmpty();
    }
}