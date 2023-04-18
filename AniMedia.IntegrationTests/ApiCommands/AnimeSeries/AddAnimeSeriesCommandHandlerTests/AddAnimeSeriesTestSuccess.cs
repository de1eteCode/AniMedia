using AniMedia.Application.ApiCommands.AnimeSeries;
using AniMedia.Application.ApiCommands.Genres;
using AniMedia.Domain.Models.Dtos;
using AniMedia.IntegrationTests.Helpers;
using FluentAssertions;
using Xunit;

namespace AniMedia.IntegrationTests.ApiCommands.AnimeSeries.AddAnimeSeriesCommandHandlerTests;

public class AddAnimeSeriesTestSuccess : IntegrationTestBase {

    [Fact]
    public override async Task Test() {
        // set user
        SetUser((await RequestAsync(CommandHelper.RegistrationCommandDe1ete())).Value!.UID);

        var availableGenres = new List<GenreDto>();

        // add genres
        for (int i = 0; i < 20; i++) {
            var genreAddCommand = new AddGenreCommand(CommandHelper.GetRandomString());
            var resAdd = await RequestAsync(genreAddCommand);
            availableGenres.Add(resAdd.Value!);
        }

        // request
        var skipGenres = Random.Shared.Next(1, availableGenres.Count);
        var takeGenres = Random.Shared.Next(skipGenres, availableGenres.Count);

        var request = new AddAnimeSeriesCommand {
            Name = CommandHelper.GetRandomString(),
            EnglishName = CommandHelper.GetRandomString(),
            JapaneseName = CommandHelper.GetRandomString(),
            Description = CommandHelper.GetRandomString(),
            Genres = availableGenres.Skip(takeGenres).Take(takeGenres).ToList()
        };

        var result = await RequestAsync(request);

        // assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.Name.Should().Be(request.Name);
        result.Value!.EnglishName.Should().Be(request.EnglishName);
        result.Value!.JapaneseName.Should().Be(request.JapaneseName);
        result.Value!.Description.Should().Be(request.Description);
        result.Value!.Genres.Count().Should().Be(request.Genres.Count());
    }
}