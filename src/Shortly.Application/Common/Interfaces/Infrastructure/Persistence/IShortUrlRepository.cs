using Shortly.Domain.Entities;

namespace Shortly.Application.Common.Interfaces.Infrastructure.Persistence
{
    public interface IShortUrlRepository
    {
        public Task<ShortUrl?> AddShortUrlAsync(ShortUrl shortUrlToAdd);
        public Task<bool> IsShortUrlExists(string shortenedUrlKey);
        public Task<List<ShortUrl>> GetAllUrlsAsync();
        public Task<string?> GetOriginalUrlAsync(string shortenedUrlKey);
        public Task<int> DeleteUrlAsync(string shortenedUrlKey);
    }
}