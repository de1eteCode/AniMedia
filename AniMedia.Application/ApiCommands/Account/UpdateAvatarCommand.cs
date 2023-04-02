using System.Transactions;
using AniMedia.Application.ApiCommands.Binary;
using AniMedia.Application.Common.Attributes;
using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AniMedia.Application.ApiCommands.Account;

[ApplicationAuthorize]
public record UpdateAvatarCommand(Stream Stream, string Name, string ContentType) : IRequest<Result<BinaryFileDto>>;

public class UpdateAvatarCommandHandler : IRequestHandler<UpdateAvatarCommand, Result<BinaryFileDto>> {
    private readonly ICurrentUserService _currentUser;
    private readonly IApplicationDbContext _context;
    private readonly IMediator _mediator;

    public UpdateAvatarCommandHandler(ICurrentUserService currentUser, IApplicationDbContext context, IMediator mediator) {
        _currentUser = currentUser;
        _context = context;
        _mediator = mediator;
    }

    public async Task<Result<BinaryFileDto>> Handle(UpdateAvatarCommand request, CancellationToken cancellationToken) {
        var user = await _context.Users
            .FirstOrDefaultAsync(e => e.UID.Equals(_currentUser.UserUID), cancellationToken);

        if (user == null) {
            return new Result<BinaryFileDto>(new EntityNotFoundError());
        }

        using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        // remove old avatar
        if (user.Avatar != null && user.AvatarFileUID != null) {
            var removeAvatarCommand = new RemoveBinaryFileCommand((Guid)user.AvatarFileUID);

            var resultRemoveAvatar = await _mediator.Send(removeAvatarCommand, cancellationToken);

            if (resultRemoveAvatar.Error != null) {
                return new Result<BinaryFileDto>(resultRemoveAvatar.Error);
            }
        }

        // set new avatar
        var saveAvatarCommand = new SaveBinaryFileCommand(request.Stream, request.Name, request.ContentType);

        var resultSaveAvatar = await _mediator.Send(saveAvatarCommand, cancellationToken);

        if (resultSaveAvatar.Error != null) {
            return new Result<BinaryFileDto>(resultSaveAvatar.Error);
        }

        // update profile
        user.AvatarFileUID = resultSaveAvatar.Value!.UID;

        await _context.SaveChangesAsync(cancellationToken);
        await _context.Entry(user).ReloadAsync(cancellationToken);

        transaction.Complete();

        return new Result<BinaryFileDto>(new BinaryFileDto(user.Avatar!));
    }
}