using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shortly.Domain.Entities;

namespace Shortly.Infrastructure.Persistence.DbContexts.EntityConfiguration
{
    public class ShortUrlConfiguration
     : IEntityTypeConfiguration<ShortUrl>
    {
        public void Configure(EntityTypeBuilder<ShortUrl> builder)
        {
            builder.HasKey(su => su.ShortenedUrlKey);

            builder.Property(su => su.ShortenedUrlKey)
                .IsRequired()
                .HasMaxLength(5);

            builder.Property(su => su.OriginalUrl)
                .IsRequired()
                .HasColumnType("text");

            builder.Property(su => su.CreationDate)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(su => su.TransitionCount)
                .IsRequired()
                .HasColumnType("int");
        }
    }
}