using Microsoft.EntityFrameworkCore;
using Shortly.Application.Common.Interfaces.Infrastructure.Persistence;
using Shortly.Domain.Entities;
using Shortly.Infrastructure.Persistence.DbContexts;

namespace Shortly.Infrastructure.Persistence.Repositories
{
    public class ShortUrlRepository : IShortUrlRepository
    {
        private readonly ShortUrlDbContext _dbContext;

        public ShortUrlRepository(ShortUrlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ShortUrl?> AddShortUrlAsync(ShortUrl shortUrlToAdd)
        {
            var addedUrl =
                await _dbContext.ShortUrls.AddAsync(shortUrlToAdd);

            await _dbContext.SaveChangesAsync();

            return addedUrl.Entity;
        }

        public async Task<bool> IsShortUrlExists(string shortenedUrlKey)
        {
            var isExists =
                await _dbContext.ShortUrls
                    .AnyAsync(url => url.ShortenedUrlKey.Equals(shortenedUrlKey));

            return isExists;
        }

        public Task<List<ShortUrl>> GetAllUrlsAsync()
        {
            var allUrls = _dbContext.ShortUrls.ToListAsync();

            return allUrls;
        }

        public async Task<string?> GetOriginalUrlAsync(string shortenedUrlKey)
        {
            var shortUrl = 
                await _dbContext.ShortUrls.FindAsync(shortenedUrlKey);

            if(shortUrl is null)
            {
                return string.Empty;   
            }

            shortUrl.TransitionCount++;

            await _dbContext.SaveChangesAsync();

            return shortUrl.OriginalUrl;
        }

        public async Task<int> DeleteUrlAsync(string shortenedUrlKey)
        {
            int deletedUrlsCount =
                await _dbContext.ShortUrls
                    .Where(url => url.ShortenedUrlKey.Equals(shortenedUrlKey))
                    .ExecuteDeleteAsync();

            return deletedUrlsCount;
        }
    }
}