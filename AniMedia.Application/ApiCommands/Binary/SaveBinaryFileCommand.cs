using System.Security.Cryptography;
using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Entities;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using MediatR;

namespace AniMedia.Application.ApiCommands.Binary;

/// <summary>
/// Сохранение файла
/// </summary>
/// <param name="Stream">Данные</param>
/// <param name="Name">Имя файла</param>
/// <param name="ContentType">Тип файла</param>
/// <param name="Md5Hash">MD5 хеш файла</param>
/// <returns>Информация о файле</returns>
public record SaveBinaryFileCommand
    (Stream Stream, string Name, string ContentType, string Md5Hash) : IRequest<Result<BinaryFileDto>>;

public class SaveBinaryFileCommandHandler : IRequestHandler<SaveBinaryFileCommand, Result<BinaryFileDto>> {
    private readonly IApplicationDbContext _context;
    private readonly IDirectoryService _dirService;

    public SaveBinaryFileCommandHandler(IApplicationDbContext context, IDirectoryService dirService) {
        _context = context;
        _dirService = dirService;
    }

    public async Task<Result<BinaryFileDto>>
        Handle(SaveBinaryFileCommand request, CancellationToken cancellationToken) {
        string hash;
        var pathToFile = _dirService.GetNewRandomPathBinaryFile();

        await using (var stream = File.Open(pathToFile, FileMode.CreateNew, FileAccess.ReadWrite)) {
            await request.Stream.CopyToAsync(stream, cancellationToken);
            stream.Seek(0, SeekOrigin.Begin);

            using var md5 = MD5.Create();
            var md5Hash = await md5.ComputeHashAsync(stream, cancellationToken);
            hash = BitConverter.ToString(md5Hash);
        }

        var fInfo = new FileInfo(pathToFile);

        if (hash.Equals(request.Md5Hash) == false) {
            await Task.Factory.StartNew(fInfo.Delete, cancellationToken);

            return new Result<BinaryFileDto>(
                new BinaryFileError("The result of the MD5 hash function differs from the declared one"));
        }

        var binFile = new BinaryFileEntity(request.Name, pathToFile, request.ContentType, fInfo.Length, hash);

        _context.BinaryFiles.Add(binFile);
        await _context.SaveChangesAsync(cancellationToken);

        return new Result<BinaryFileDto>(new BinaryFileDto(binFile));
    }
}