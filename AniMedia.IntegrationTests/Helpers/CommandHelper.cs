using AniMedia.Application.ApiCommands.Auth;

namespace AniMedia.IntegrationTests.Helpers;

public static class CommandHelper {

    public static RegistrationCommand RegistrationDe1ete() => new RegistrationCommand(
        Nickname: "de1ete",
        Password: "password",
        Ip: "127.0.0.1",
        UserAgent: "VisualStudio 2022");

    public static RegistrationCommand RegistrationCommon() => new RegistrationCommand(
        Nickname: "common",
        Password: "edsregtertgert",
        Ip: "212.23.4.243",
        UserAgent: "VisualStudio 2022");
}