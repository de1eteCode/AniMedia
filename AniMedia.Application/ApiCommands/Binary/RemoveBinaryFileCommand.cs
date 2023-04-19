using System.Transactions;
using AniMedia.Application.ApiQueries.Binary;
using AniMedia.Application.Common.Attributes;
using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AniMedia.Application.ApiCommands.Binary;

/// <summary>
/// Удаление файла
/// </summary>
/// <param name="BinaryFileUidOrName">Идентификатор файла или его полное наименование</param>
[ApplicationAuthorize]
public record RemoveBinaryFileCommand(string BinaryFileUidOrName) : IRequest<Result<BinaryFileDto>>;

public class RemoveBinaryFileCommandHandler : IRequestHandler<RemoveBinaryFileCommand, Result<BinaryFileDto>> {
    private readonly IApplicationDbContext _context;
    private readonly IMediator _mediator;

    public RemoveBinaryFileCommandHandler(IApplicationDbContext context, IMediator mediator) {
        _context = context;
        _mediator = mediator;
    }

    public async Task<Result<BinaryFileDto>> Handle(RemoveBinaryFileCommand request, CancellationToken cancellationToken) {
        var entity = await _mediator.Send(new GetBinaryFileResponseQueryCommand(request.BinaryFileUidOrName));

        if (entity.IsSuccess == false || entity.Error != null) {
            return new Result<BinaryFileDto>(entity.Error ?? new EntityNotFoundError());
        }
        
        using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var infoFile = new FileInfo(entity.Value!.PathFile);
        await Task.Factory.StartNew(infoFile.Delete, cancellationToken);

        _context.BinaryFiles.Remove(entity.Value!);
        await _context.SaveChangesAsync(cancellationToken);

        transaction.Complete();

        return new Result<BinaryFileDto>(new BinaryFileDto(entity.Value!));
    }
}

public class RemoveBinaryFileCommandValidator : AbstractValidator<RemoveBinaryFileCommand> {

    public RemoveBinaryFileCommandValidator() {
        RuleFor(e => e.BinaryFileUidOrName).NotEmpty();
    }
}