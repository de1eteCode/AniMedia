using FluentValidation;

namespace AniMedia.Domain.Entities.Validations;

public class GenreEntityValidator : BaseAuditableEntityValidator<GenreEntity> {
    public GenreEntityValidator() {
        RuleFor(e => e.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(64);
    }
}