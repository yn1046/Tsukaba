using System;
using Microsoft.EntityFrameworkCore;
using Tsukaba.Models.DatabaseModels;
using Npgsql;

namespace Tsukaba.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Board> Boards { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Image> Images { get; set; }

        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=BoardDB;Username=sky;Password=password");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Board>().HasData(new Board { Id = 1, Title = "/b/", FullTitle = "Бардак" });
            modelBuilder.Entity<Post>().HasData(
                new Post
                {
                    Id = 1,
                    NumberOnBoard = 1,
                    Title = "Тохо-тред",
                    Message = @" Как минимум 74,67% девочек в Генсокё - украинки
                    На тох посмотришь, так они там все украинки.",
                    Time = DateTime.Now,
                    LastTimeBumped = DateTime.Now,
                    BoardId = 1
                },
                new Post
                {
                    Id = 2,
                    NumberOnBoard = 2,
                    Title = "Представь ситуацию",
                    Message = "Идёшь домой по тёмному переулку и видишь пикрилейтед. Твои действия?",
                    Time = DateTime.Now.AddMinutes(5),
                    LastTimeBumped = DateTime.Now.AddMinutes(5),
                    BoardId = 1
                }
            );
            modelBuilder.Entity<Image>().HasData(
                new Image
                {
                    Id = 1,
                    ImageUrl = "Baka.jpg",
                    PostId = 1
                },
                new Image
                {
                    Id = 2,
                    ImageUrl = "Cirno.gif",
                    PostId = 2
                }
            );
        }
    }
}