using FluentValidation;

namespace AniMedia.Domain.Entities.Validations;

public class RateAnimeSeriesEntityValidator : BaseAuditableEntityValidator<RateAnimeSeriesEntity> {

    public RateAnimeSeriesEntityValidator() {
        RuleFor(e => e.Rate).InclusiveBetween((byte)1, (byte)10);
        RuleFor(e => e.UserUid).NotEmpty();
        RuleFor(e => e.AnimeSeriesUid).NotEmpty();
    }
}