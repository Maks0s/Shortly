namespace Shortly.Application.Common.Interfaces.Application
{
    public interface IUrlShorteningService
    {
        public Task<string> GenerateShortenedUrlKey();
    }
}