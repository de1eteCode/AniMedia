using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Entities;
using AniMedia.Domain.Models.Responses;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AniMedia.Application.ApiQueries.Binary;

public record GetBinaryFileResponseQueryCommand(string BinaryFileUidOrName) : IRequest<Result<BinaryFileEntity>>;

public class GetBinaryFileResponseQueryCommandHandler : IRequestHandler<GetBinaryFileResponseQueryCommand, Result<BinaryFileEntity>> {
    private readonly IApplicationDbContext _context;

    public GetBinaryFileResponseQueryCommandHandler(IApplicationDbContext context) {
        _context = context;
    }

    public async Task<Result<BinaryFileEntity>> Handle(GetBinaryFileResponseQueryCommand request, CancellationToken cancellationToken) {
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
            return new Result<BinaryFileEntity>(new EntityNotFoundError());
        }

        return new Result<BinaryFileEntity>(entity);
    }
}

public class GetBinaryFileResponseQueryCommandValidator : AbstractValidator<GetBinaryFileResponseQueryCommand> {

    public GetBinaryFileResponseQueryCommandValidator() {
        RuleFor(e => e.BinaryFileUidOrName).NotEmpty();
    }
}