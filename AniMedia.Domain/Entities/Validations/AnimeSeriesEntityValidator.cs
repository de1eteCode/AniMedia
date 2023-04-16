using FluentValidation;

namespace AniMedia.Domain.Entities.Validations;

public class AnimeSeriesEntityValidator : BaseAuditableEntityValidator<AnimeSeriesEntity> {
    
    public AnimeSeriesEntityValidator() {
        RuleFor(e => e.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(512);
    }
}