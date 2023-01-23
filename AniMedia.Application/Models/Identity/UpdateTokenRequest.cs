namespace AniMedia.Application.Models.Identity;

public class UpdateTokenRequest {
    public required TokenPair Tokens { get; init; }
}

public class UpdateTokenResponce {
    public required TokenPair Tokens { get; init; }
}