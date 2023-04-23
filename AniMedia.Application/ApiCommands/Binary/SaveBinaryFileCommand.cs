using System.Security.Cryptography;
using System.Transactions;
using AniMedia.Application.Common.Attributes;
using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Entities;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using FluentValidation;
using MediatR;

namespace AniMedia.Application.ApiCommands.Binary;

/// <summary>
/// Сохранение файла
/// </summary>
/// <param name="Stream">Данные</param>
/// <param name="ContentType">Тип файла</param>
public record SaveBinaryFileCommand(Stream Stream, string ContentType) : IRequest<Result<BinaryFileDto>>;

public class SaveBinaryFileCommandHandler : IRequestHandler<SaveBinaryFileCommand, Result<BinaryFileDto>> {
    private readonly IApplicationDbContext _context;
    private readonly IDirectoryService _dirService;

    public SaveBinaryFileCommandHandler(IApplicationDbContext context, IDirectoryService dirService) {
        _context = context;
        _dirService = dirService;
    }

    public async Task<Result<BinaryFileDto>> Handle(SaveBinaryFileCommand request, CancellationToken cancellationToken) {
        MimeKit.MimeTypes.TryGetExtension(request.ContentType, out var extension);

        string hash;

        var pathToFile = _dirService.GetNewRandomPathBinaryFile(string.IsNullOrEmpty(extension) ? "bin" : extension);

        using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await using (var stream = File.Open(pathToFile, FileMode.CreateNew, FileAccess.ReadWrite)) {
            await request.Stream.CopyToAsync(stream, cancellationToken);
            stream.Seek(0, SeekOrigin.Begin);

            using var md5 = MD5.Create();
            var md5Hash = await md5.ComputeHashAsync(stream, cancellationToken);
            hash = BitConverter.ToString(md5Hash);
        }

        var fInfo = new FileInfo(pathToFile);

        var binFile = new BinaryFileEntity(fInfo.Name, pathToFile, request.ContentType, fInfo.Length, hash);

        _context.BinaryFiles.Add(binFile);
        await _context.SaveChangesAsync(cancellationToken);

        transaction.Complete();

        return new Result<BinaryFileDto>(new BinaryFileDto(binFile));
    }
}

public class SaveBinaryFileCommandValidator : AbstractValidator<SaveBinaryFileCommand> {

    public SaveBinaryFileCommandValidator() {
        RuleFor(e => e.Stream).NotNull();
        RuleFor(e => e.ContentType).NotEmpty();
    }
}