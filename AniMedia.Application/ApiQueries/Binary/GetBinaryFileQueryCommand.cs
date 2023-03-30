using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AniMedia.Application.ApiQueries.Binary;

public record GetBinaryFileQueryCommand(Guid BinaryFileUid) : IRequest<Result<BinaryFileDto>>;

public class GetBinaryFileQueryCommandHandler : IRequestHandler<GetBinaryFileQueryCommand, Result<BinaryFileDto>> {
    private readonly IApplicationDbContext _context;

    public GetBinaryFileQueryCommandHandler(IApplicationDbContext context) {
        _context = context;
    }

    public async Task<Result<BinaryFileDto>> Handle(GetBinaryFileQueryCommand request,
        CancellationToken cancellationToken) {
        var binFile =
            await _context.BinaryFiles.FirstOrDefaultAsync(e => e.UID.Equals(request.BinaryFileUid), cancellationToken);

        if (binFile == null) return new Result<BinaryFileDto>(new EntityNotFoundError());

        return new Result<BinaryFileDto>(new BinaryFileDto(binFile));
    }
}