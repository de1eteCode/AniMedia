using AniMedia.Application.Common.Interfaces;
using AniMedia.Domain.Models.Dtos;
using AniMedia.Domain.Models.Responses;
using MediatR;

namespace AniMedia.Application.ApiQueries.Account;
public record GetProfileQueryCommand() : IRequest<Result<ProfileUserDto>>;

public class GetProfileQueryCommandHandler : IRequestHandler<GetProfileQueryCommand, Result<ProfileUserDto>> {
    private readonly ICurrentUserService _currentUser;
    private readonly IApplicationDbContext _context;

    public GetProfileQueryCommandHandler(ICurrentUserService currentUser, IApplicationDbContext context) {
        _currentUser = currentUser;
        _context = context;
    }

    public async Task<Result<ProfileUserDto>> Handle(GetProfileQueryCommand request, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }
}