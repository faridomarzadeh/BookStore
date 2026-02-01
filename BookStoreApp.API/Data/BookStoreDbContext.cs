using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Data;

public partial class BookStoreDbContext : IdentityDbContext<ApiUser>
{
    public BookStoreDbContext()
    {
    }

    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Authors__3214EC07E52240B6");

            entity.Property(e => e.Bio).HasMaxLength(250);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Books__3214EC07C044C540");

            entity.HasIndex(e => e.Isbn, "UQ_ISBN").IsUnique();

            entity.Property(e => e.Image).HasMaxLength(50);
            entity.Property(e => e.Isbn)
                .HasMaxLength(50)
                .HasColumnName("ISBN");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Summary).HasMaxLength(250);
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK_Books_Authors");
        });

        modelBuilder.Entity<IdentityRole>().HasData
            (
            new IdentityRole
            {
                Name="User",
                NormalizedName= "USER",
                Id= "bd0422bb-c842-401e-ab11-0aa4545a8b4b"
            },
            new IdentityRole
            {
                Name="Administrator",
                NormalizedName= "ADMINISTRATOR",
                Id= "159fb287-1bca-4bd6-bfc6-e69f0bdacd47"
            }
            );
        var hasher = new PasswordHasher<ApiUser>();
        modelBuilder.Entity<ApiUser>().HasData
            (
            new ApiUser
            {
                Id = "442b5852-0a3e-4bc3-8aba-a4c957ba4b55",
                Email = "admin@bookstore.com",
                NormalizedEmail = "ADMIN@BOOKSTORE.COM",
                UserName = "admin@bookstore.com",
                NormalizedUserName = "ADMIN@BOOKSTORE.COM",
                FirstName = "System",
                LastName = "Admin",
                PasswordHash = hasher.HashPassword(null, "Pa$$w0rd")
            },
            new ApiUser
            {
                Id = "4b562b69-831e-46f2-8ffd-6cbae43bb4bd",
                Email = "user@bookstore.com",
                NormalizedEmail = "USER@BOOKSTORE.COM",
                UserName = "user@bookstore.com",
                NormalizedUserName = "USER@BOOKSTORE.COM",
                FirstName = "System",
                LastName = "User",
                PasswordHash = hasher.HashPassword(null, "Pa$$w0rd")
            }
            );
        modelBuilder.Entity<IdentityUserRole<string>>().HasData
            (
                new IdentityUserRole<string>
                {
                    RoleId = "bd0422bb-c842-401e-ab11-0aa4545a8b4b",
                    UserId = "4b562b69-831e-46f2-8ffd-6cbae43bb4bd"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "159fb287-1bca-4bd6-bfc6-e69f0bdacd47",
                    UserId = "442b5852-0a3e-4bc3-8aba-a4c957ba4b55"
                }
            );
        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
