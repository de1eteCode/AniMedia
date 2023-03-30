using System.Security.Cryptography;
using System.Text;
using AniMedia.Application.Common.Interfaces;

namespace AniMedia.Application.Common.Services;

public class HashService : IHashService {

    public string Hmacsha512CryptoHash(string value, out string salt) {
        var hmac = new HMACSHA512();
        var bytes = Encoding.UTF8.GetBytes(value);
        var computeHash = hmac.ComputeHash(bytes);
        salt = Convert.ToBase64String(hmac.Key);

        return Convert.ToBase64String(computeHash);
    }

    public string Hmacsha512CryptoHashWithSalt(string value, string salt) {
        var key = Convert.FromBase64String(salt);
        var hmac = new HMACSHA512 { Key = key };
        var bytes = Encoding.UTF8.GetBytes(value);
        var computeHash = hmac.ComputeHash(bytes);

        return Convert.ToBase64String(computeHash);
    }
}