using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Entities;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using MediatR;
using System.Security.Cryptography;

namespace AniMedia.Application.ApiCommands.Binary;

/// <summary>
/// Сохранение файла
/// </summary>
/// <param name="Stream">Данные</param>
/// <param name="Name">Имя файла</param>
/// <param name="ContentType">Тип файла</param>
/// <param name="Length">Размер</param>
/// <returns>Информация о файле</returns>
public record SaveBinaryFileCommand(Stream Stream, string Name, string ContentType, long Length) : IRequest<Result<BinaryFileDto>>;

public class SaveBinaryFileCommandHandler : IRequestHandler<SaveBinaryFileCommand, Result<BinaryFileDto>> {
    private readonly IApplicationDbContext _context;

    public SaveBinaryFileCommandHandler(IApplicationDbContext context) {
        _context = context;
    }

    public async Task<Result<BinaryFileDto>> Handle(SaveBinaryFileCommand request, CancellationToken cancellationToken) {
        string hash = string.Empty;

        using (var stream = File.Open(request.Name, FileMode.CreateNew)) {
            await request.Stream.CopyToAsync(stream, cancellationToken);
            stream.Position = 0;

            using var md5 = MD5.Create();
            var md5Hash = await md5.ComputeHashAsync(stream, cancellationToken);
            hash = BitConverter.ToString(md5Hash);
        }

        var binFile = new BinaryFileEntity(request.Name, request.ContentType, request.Length, hash);

        _context.BinaryFiles.Add(binFile);
        await _context.SaveChangesAsync(cancellationToken);

        return new Result<BinaryFileDto>(new BinaryFileDto(binFile));
    }
}