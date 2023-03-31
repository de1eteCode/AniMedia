using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Entities;
using AniMedia.Domain.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AniMedia.Application.ApiQueries.Binary;

public record GetBinaryFileResponseQueryCommand(Guid BinaryFileUid) : IRequest<Result<BinaryFileEntity>>;

public class GetBinaryFileResponseQueryCommandHandler : IRequestHandler<GetBinaryFileResponseQueryCommand, Result<BinaryFileEntity>> {
    private readonly IApplicationDbContext _context;

    public GetBinaryFileResponseQueryCommandHandler(IApplicationDbContext context) {
        _context = context;
    }

    public async Task<Result<BinaryFileEntity>> Handle(GetBinaryFileResponseQueryCommand request, CancellationToken cancellationToken) {
        var entity = await _context.BinaryFiles
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.UID.Equals(request.BinaryFileUid), cancellationToken);

        if (entity == null) {
            return new Result<BinaryFileEntity>(new EntityNotFoundError());
        }

        return new Result<BinaryFileEntity>(entity);
    }
}