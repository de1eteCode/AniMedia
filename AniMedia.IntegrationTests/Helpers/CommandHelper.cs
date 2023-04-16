using AniMedia.Application.ApiCommands.Auth;
using AniMedia.Domain.Models.Auth.Requests;

namespace AniMedia.IntegrationTests.Helpers;

public static class CommandHelper {

    public static RegistrationCommand RegistrationCommandDe1ete() {
        return new RegistrationCommand(
            RegistrationRequestDe1ete(),
            "127.0.0.1",
            "VisualStudio 2022");
    }

    public static RegistrationRequest RegistrationRequestDe1ete() {
        return new RegistrationRequest() {
            Nickname = "de1ete",
            Password = "password"
        };
    }

    public static LoginRequest LoginRequestDe1ete() {
        return new LoginRequest() {
            Nickname = "de1ete",
            Password = "password"
        };
    }

    public static RegistrationCommand RegistrationCommon() {
        return new RegistrationCommand(
            RegistrationRequestCommon(),
            "212.23.4.243",
            "VisualStudio 2022");
    }

    public static RegistrationRequest RegistrationRequestCommon() {
        return new RegistrationRequest() {
            Nickname = "common",
            Password = "edsregtertgert"
        };
    }

    public static LoginRequest LoginRequestCommon() {
        return new LoginRequest() {
            Nickname = "common",
            Password = "edsregtertgert"
        };
    }

    public static string RandomIpAddress() {
        return $"{Random.Shared.Next(1, 255)}.{Random.Shared.Next(1, 255)}.{Random.Shared.Next(1, 255)}.{Random.Shared.Next(1, 255)}";
    }

    public static string GetRandomString() {
        var rnd = Random.Shared;
        var repeat = rnd.Next(3, 20);
        return new string(Enumerable.Repeat(1, repeat).Select(_ => (char)rnd.Next('A', 'z')).ToArray());
    }
}