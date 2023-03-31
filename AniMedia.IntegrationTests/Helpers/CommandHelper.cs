using AniMedia.Application.ApiCommands.Auth;

namespace AniMedia.IntegrationTests.Helpers;

public static class CommandHelper {

    public static RegistrationCommand RegistrationDe1ete() {
        return new RegistrationCommand(
            "de1ete",
            "password",
            "127.0.0.1",
            "VisualStudio 2022");
    }

    public static RegistrationCommand RegistrationCommon() {
        return new RegistrationCommand(
            "common",
            "edsregtertgert",
            "212.23.4.243",
            "VisualStudio 2022");
    }

    public static string RandomIpAddress() {
        return $"{Random.Shared.Next(1, 255)}.{Random.Shared.Next(1, 255)}.{Random.Shared.Next(1, 255)}.{Random.Shared.Next(1, 255)}";
    }
}