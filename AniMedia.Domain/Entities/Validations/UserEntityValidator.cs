using FluentValidation;

namespace AniMedia.Domain.Entities.Validations;

public class UserEntityValidator : AbstractValidator<UserEntity> {

    public UserEntityValidator() {
        RuleFor(e => e.UID).NotEmpty();
        RuleFor(e => e.Nickname).NotEmpty().Length(3, 20);
        RuleFor(e => e.FirstName).MaximumLength(50);
        RuleFor(e => e.SecondName).MaximumLength(50);
        RuleFor(e => e.PasswordHash).NotEmpty();
        RuleFor(x => x.PasswordSalt).NotEmpty();
    }
}