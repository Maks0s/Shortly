﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shortly.Infrastructure.Persistence.DbContexts;

#nullable disable

namespace Shortly.Infrastructure.Persistence.Migrations.ShortUrlTablesMigrations
{
    [DbContext(typeof(ShortUrlDbContext))]
    [Migration("20241007113829_ReducedShortenedUrlKeyLength")]
    partial class ReducedShortenedUrlKeyLength
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("ShortUrl")
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Shortly.Domain.Entities.ShortUrl", b =>
                {
                    b.Property<string>("ShortenedUrlKey")
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime");

                    b.Property<string>("OriginalUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TransitionCount")
                        .HasColumnType("int");

                    b.HasKey("ShortenedUrlKey");

                    b.ToTable("ShortUrls", "ShortUrl");
                });
#pragma warning restore 612, 618
        }
    }
}
