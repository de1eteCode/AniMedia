using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Entities;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AniMedia.Application.ApiQueries.Binary;

/// <summary>
/// Команда получения бинарного файла
/// </summary>
/// <param name="BinaryFileUidOrName">Идентификатор файла или его полное наименование</param>
public record GetBinaryFileQueryCommand(string BinaryFileUidOrName) : IRequest<Result<BinaryFileDto>>;

public class GetBinaryFileQueryCommandHandler : IRequestHandler<GetBinaryFileQueryCommand, Result<BinaryFileDto>> {
    private readonly IApplicationDbContext _context;

    public GetBinaryFileQueryCommandHandler(IApplicationDbContext context) {
        _context = context;
    }

    public async Task<Result<BinaryFileDto>> Handle(GetBinaryFileQueryCommand request, CancellationToken cancellationToken) {
        var wasGuid = Guid.TryParse(request.BinaryFileUidOrName, out var id);

        var entity = default(BinaryFileEntity?);

        if (wasGuid) {
            entity = await _context.BinaryFiles
                .FirstOrDefaultAsync(e => e.UID.Equals(id), cancellationToken);
        }
        else {
            entity = await _context.BinaryFiles
                .FirstOrDefaultAsync(e => e.Name.Equals(request.BinaryFileUidOrName), cancellationToken);
        }

        if (entity == null) {
            return new Result<BinaryFileDto>(new EntityNotFoundError());
        }

        return new Result<BinaryFileDto>(new BinaryFileDto(entity));
    }
}

public class GetBinaryFileQueryCommandValidator : AbstractValidator<GetBinaryFileQueryCommand> {

    public GetBinaryFileQueryCommandValidator() {
        RuleFor(e => e.BinaryFileUidOrName).NotEmpty();
    }
}