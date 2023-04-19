using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AniMedia.Application.ApiQueries.Account;

/// <summary>
/// Получение профиля пользователя
/// </summary>
/// <param name="UserUid">Идентификатор пользователя</param>
public record GetProfileQueryCommand(Guid UserUid) : IRequest<Result<ProfileUserDto>>;

public class GetProfileQueryCommandHandler : IRequestHandler<GetProfileQueryCommand, Result<ProfileUserDto>> {
    private readonly IApplicationDbContext _context;

    public GetProfileQueryCommandHandler(IApplicationDbContext context) {
        _context = context;
    }

    public async Task<Result<ProfileUserDto>> Handle(GetProfileQueryCommand request, CancellationToken cancellationToken) {
        var user = await _context.Users
            .FirstOrDefaultAsync(e => e.UID.Equals(request.UserUid), cancellationToken);

        if (user == null) {
            return new Result<ProfileUserDto>(new EntityNotFoundError());
        }

        return new Result<ProfileUserDto>(new ProfileUserDto(user));
    }
}

public class GetProfileQueryCommandValidator : AbstractValidator<GetProfileQueryCommand> {

    public GetProfileQueryCommandValidator() {
        RuleFor(e => e.UserUid).NotEmpty();
    }
}