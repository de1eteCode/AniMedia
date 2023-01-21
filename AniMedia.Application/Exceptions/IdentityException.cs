using Microsoft.AspNetCore.Identity;
using System.Runtime.Serialization;

namespace AniMedia.Application.Exceptions;

public class IdentityException : ApplicationException {
    private readonly List<string> _errors = new List<string>();

    public IReadOnlyCollection<string> Errors => _errors;

    public IdentityException(IdentityResult identityResult) {
        _errors.AddRange(identityResult.Errors.Select(e => e.Description));
    }

    public IdentityException() {
    }

    public IdentityException(SerializationInfo info, StreamingContext context) : base(info, context) {
    }

    public IdentityException(string? message) : base(message) {
    }

    public IdentityException(string? message, Exception? innerException) : base(message, innerException) {
    }
}