namespace AniMedia.API.Tests.Helpers; 

public static class CommandHelper {
    
    public static string RandomIpAddress() {
        return $"{Random.Shared.Next(1, 255)}.{Random.Shared.Next(1, 255)}.{Random.Shared.Next(1, 255)}.{Random.Shared.Next(1, 255)}";
    }

    public static string GetRandomString(int minCharacters = 3) {
        if (minCharacters < 1) {
            throw new ArgumentOutOfRangeException(nameof(minCharacters));
        }
        
        var rnd = Random.Shared;
        var repeat = rnd.Next(minCharacters, 20);
        return new string(Enumerable.Repeat(1, repeat).Select(_ => (char)rnd.Next('A', 'z')).ToArray());
    }
}