namespace AniMedia.API.Tests.Helpers; 

public static class CommandHelper {
    
    public static string RandomIpAddress() {
        return $"{Random.Shared.Next(1, 255)}.{Random.Shared.Next(1, 255)}.{Random.Shared.Next(1, 255)}.{Random.Shared.Next(1, 255)}";
    }

    public static string GetRandomString(int minCharacters = 3, int maxCharacters = 20) {
        if (minCharacters < 1) {
            throw new ArgumentOutOfRangeException(nameof(minCharacters));
        }

        if (maxCharacters < minCharacters) {
            throw new ArgumentOutOfRangeException(nameof(maxCharacters));
        }
        
        var rnd = Random.Shared;
        var repeat = rnd.Next(minCharacters, maxCharacters);
        return new string(Enumerable.Repeat(1, repeat).Select(_ => (char)rnd.Next('A', 'z')).ToArray());
    }
}