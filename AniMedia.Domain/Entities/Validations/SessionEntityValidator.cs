using FluentValidation;

namespace AniMedia.Domain.Entities.Validations;

public class SessionEntityValidator : AbstractValidator<SessionEntity> {

    public SessionEntityValidator() {
        RuleFor(x => x.UID).NotEmpty();
        RuleFor(x => x.UserUid).NotEmpty();
        RuleFor(x => x.AccessToken).NotEmpty();
        RuleFor(x => x.RefreshToken).NotNull();
        RuleFor(x => x.Ip).NotNull();
        RuleFor(x => x.UserAgent).NotNull();
        RuleFor(x => x.ExpiresAt).NotNull();
        RuleFor(x => x.CreateAt).NotNull();
    }
}