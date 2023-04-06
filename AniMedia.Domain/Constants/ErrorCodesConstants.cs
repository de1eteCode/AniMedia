namespace AniMedia.Domain.Constants; 

public static class ErrorCodesConstants {
    public const int NoSetCode = -1;

    public const int Exist = 39000001;
    
    public const int NotFound = 4000000;
    public const int NotFoundUser = 4000002;
    public const int NotFoundSession = 4000003;
    
    public const int AuthInvalidToken = 1000001;
    public const int AuthNotFoundUserIdInToken = 1000002;
    public const int AuthInvalidPassword = 1000003;
    public const int AuthExpired = 1000004;
}