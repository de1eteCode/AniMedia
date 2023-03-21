using AniMedia.Application.Common.Models.Responses;
using MediatR;

namespace AniMedia.Application.ApiCommands.Auth;
public record LoginCommand(string Nickname, string Password, string Ip, string UserAgent) : IRequest<Result<AuthorizationResponse>>;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<AuthorizationResponse>> {

    public Task<Result<AuthorizationResponse>> Handle(LoginCommand request, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }
}