using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace AniMedia.API.Tests.Helpers; 

public static class DummyHttpExtensions {

    private const string FakeRemoteIpHeader = "FakeRemote";
    
    /// <summary>
    /// Добавление IP адреса в заголовок
    /// </summary>
    /// <param name="client"></param>
    /// <param name="ipAddress"></param>
    public static void SetRemoteIp(this HttpClient client, string ipAddress = "127.0.0.1") {
        if (IPAddress.TryParse(ipAddress, out _) == false) {
            throw new ArgumentException("Not valid ip address");
        }

        if (client.DefaultRequestHeaders.Contains(FakeRemoteIpHeader)) {
            client.DefaultRequestHeaders.Remove(FakeRemoteIpHeader);
        }

        client.DefaultRequestHeaders.Add(FakeRemoteIpHeader, ipAddress);
    }
    
    /// <summary>
    /// Получение фальшивого IP адреса из заголовка
    /// </summary>
    /// <param name="context"></param>
    /// <returns>IP адрес или Empty</returns>
    public static string ParseRemoteIpFromHeader(this HttpContext context) {
        if (context.Request.Headers.TryGetValue(FakeRemoteIpHeader, out var ipStr)) {
            return ipStr!;
        }
        
        return string.Empty;
    }

    /// <summary>
    /// Установка юзер агента в заголовок
    /// </summary>
    /// <param name="client"></param>
    /// <param name="userAgent"></param>
    public static void SetUserAgent(this HttpClient client, string userAgent = "AniMediaApiTest/1.0") {
        client.DefaultRequestHeaders.Add(HeaderNames.UserAgent, userAgent);
    }

    /// <summary>
    /// Установка токена в заголовок
    /// </summary>
    /// <param name="client"></param>
    /// <param name="jwtToken"></param>
    public static void SetAuthorizationToken(this HttpClient client, string jwtToken) {
        if (client.DefaultRequestHeaders.Contains(HeaderNames.Authorization)) {
            client.DefaultRequestHeaders.Remove(HeaderNames.Authorization);
        }
        
        client.DefaultRequestHeaders.Add(HeaderNames.Authorization, "Bearer " + jwtToken);
    }
}