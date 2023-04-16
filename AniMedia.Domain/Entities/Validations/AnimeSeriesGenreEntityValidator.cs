using FluentValidation;

namespace AniMedia.Domain.Entities.Validations;

public class AnimeSeriesGenreEntityValidator : BaseAuditableEntityValidator<AnimeSeriesGenreEntity> {
    
    public AnimeSeriesGenreEntityValidator() {
        RuleFor(e => e.GenreUid).NotEmpty();
        RuleFor(e => e.AnimeSeriesUid).NotEmpty();
    }
}