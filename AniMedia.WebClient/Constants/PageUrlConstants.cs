namespace AniMedia.WebClient.Constants;

public static class PageUrlConstants {
    public const string PrefixAdminPanel = "/admin";

    public const string LoginPageUrl = "/account/login";
    public const string RegisterPageUrl = "/account/register";
    public const string ProfilePageUrl = "/account/profile";
    public const string IndexPageUrl = "/";
    public const string CounterPageUrl = "/counter";

    public const string AdminIndexPageUrl = PrefixAdminPanel + IndexPageUrl;
    public const string SwaggerPageUrl = PrefixAdminPanel + "/swagger";
    public const string ManageUsersPageUrl = PrefixAdminPanel + "/users";
    public const string ManageAnimeSeriesPageUrl = PrefixAdminPanel + "/animeseries";
    public const string ManageRulesPageUrl = PrefixAdminPanel + "/rules";
}