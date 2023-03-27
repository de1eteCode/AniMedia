using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AniMedia.Application.ApiCommands.Binary;

public record RemoveBinaryFileCommand(Guid BinaryFileUid) : IRequest<Result<BinaryFileDto>>;

public class RemoveBinaryFileCommandHandler : IRequestHandler<RemoveBinaryFileCommand, Result<BinaryFileDto>> {
    private readonly IApplicationDbContext _context;

    public RemoveBinaryFileCommandHandler(IApplicationDbContext context) {
        _context = context;
    }

    public async Task<Result<BinaryFileDto>> Handle(RemoveBinaryFileCommand request, CancellationToken cancellationToken) {
        var binFile = await _context.BinaryFiles.FirstOrDefaultAsync(e => e.UID.Equals(request.BinaryFileUid), cancellationToken);

        if (binFile == null) {
            return new Result<BinaryFileDto>(new EntityNotFoundError());
        }

        var infoFile = new FileInfo(binFile.PathFile);
        await Task.Factory.StartNew(infoFile.Delete, cancellationToken);

        _context.BinaryFiles.Remove(binFile);
        await _context.SaveChangesAsync(cancellationToken);

        return new Result<BinaryFileDto>(new BinaryFileDto(binFile));
    }
}