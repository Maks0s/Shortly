using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shortly.Infrastructure.Persistence.Migrations.ShortUrlTablesMigrations
{
    /// <inheritdoc />
    public partial class AddShortenedUrlField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortenedUrl",
                schema: "ShortUrl",
                table: "ShortUrls",
                type: "text",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortenedUrl",
                schema: "ShortUrl",
                table: "ShortUrls");
        }
    }
}
