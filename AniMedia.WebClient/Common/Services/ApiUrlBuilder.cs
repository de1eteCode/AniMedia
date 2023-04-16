﻿using AniMedia.WebClient.Common.Contracts;

namespace AniMedia.WebClient.Common.Services; 

public class ApiUrlBuilder : IApiUrlBuilder {

    private readonly IConfiguration _configuration;

    public ApiUrlBuilder(IConfiguration configuration) {
        _configuration = configuration;
    }

    public string GetMediaFileUrl(Guid uid) {
        return $"{GetBaseUrl()}/api/v1/media/file/{uid}";
    }

    public string GetSwaggerUrl() {
        return GetBaseUrl() + "/swagger";
    }

    private string GetBaseUrl() {
        return _configuration["ApiServiceUrl"]!;
    }
}