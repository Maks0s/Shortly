using Shortly.Application.Common.Interfaces.Application.Services;
using Shortly.Application.Common.Interfaces.Infrastructure.Persistence;

namespace Shortly.Application.ShortUrls.Services
{
    public class UrlShorteningService : IUrlShorteningService
    {
        private const int ShortUrlLength = 5;
        private const string Chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        private readonly Random _random;

        private readonly IShortUrlRepository _shortUrlRepository;

        public UrlShorteningService(IShortUrlRepository shortUrlRepository)
        {
            _random = new Random();
            _shortUrlRepository = shortUrlRepository;
        }

        public async Task<string> GenerateShortenedUrlKeyAsync()
        {
            var keyChars = new char[ShortUrlLength];

            while (true)
            {
                for(int i = 0; i < ShortUrlLength; i++)
                {
                    var randomCharIndex = _random.Next(Chars.Length - 1);

                    keyChars[i] = Chars[randomCharIndex];
                }

                string urlKey = new string(keyChars);

                if(!await _shortUrlRepository.IsShortUrlExists(urlKey))
                {
                    return urlKey;
                }
            }
        }
    }
}