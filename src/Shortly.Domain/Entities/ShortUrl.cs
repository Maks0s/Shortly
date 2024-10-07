namespace Shortly.Domain.Entities
{
    public class ShortUrl
    {
        public string ShortenedUrlKey { get; set; } = default!;
        public string OriginalUrl { get; set; } = default!;
        public string ShortenedUrl { get; set; } = default!;
        public DateTime CreationDate { get; set; }
        public int TransitionCount { get; set; }
    }
}