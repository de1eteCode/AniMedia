using AniMedia.Web.Services.Contracts;

namespace AniMedia.Web.Services.Base;

internal abstract class BaseService {
    protected readonly IApiClient _api;

    protected BaseService(IApiClient api) {
        _api = api;
    }
}