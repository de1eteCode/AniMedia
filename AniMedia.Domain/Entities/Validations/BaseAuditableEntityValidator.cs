using AniMedia.Domain.Abstracts;
using FluentValidation;

namespace AniMedia.Domain.Entities.Validations;

public abstract class BaseAuditableEntityValidator<TAuditableEntity> : AbstractValidator<TAuditableEntity>
    where TAuditableEntity : BaseAuditableEntity {

    protected BaseAuditableEntityValidator() {
        RuleFor(e => e.CreateAt).NotEmpty();
    }
}