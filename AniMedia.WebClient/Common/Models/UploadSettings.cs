namespace AniMedia.WebClient.Common.Models; 

public class UploadSettings {
    
    public int MaxImageSizeMb { get; init; }

    public static long ConvertMbToBytes(int size) {
        if (size < 0) {
            throw new ArgumentOutOfRangeException(nameof(size),"Size cannot be below zero");
        }
        
        return size * 1048576; // 1024*1024
    }
}