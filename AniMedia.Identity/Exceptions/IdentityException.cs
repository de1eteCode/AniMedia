using Microsoft.AspNetCore.Identity;

namespace AniMedia.Identity.Exceptions;

public class IdentityException : ApplicationException {
    private readonly List<string> _errors = new List<string>();

    public IReadOnlyCollection<string> Errors => _errors;

    public IdentityException(IdentityResult identityResult) {
        _errors.AddRange(identityResult.Errors.Select(e => e.Description));
    }
}