namespace Shortly.Application.Common.Interfaces.Application.Services
{
    public interface IUrlShorteningService
    {
        public Task<string> GenerateShortenedUrlKey();
    }
}