namespace AniMedia.WebClient.Common.Contracts; 

public interface IApiUrlBuilder {

    public string GetMediaFileUrl(Guid uid);

    public string GetSwaggerUrl();
}