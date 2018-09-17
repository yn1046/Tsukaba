using System;
using Microsoft.EntityFrameworkCore;
using Tsukaba.Models.DatabaseModels;
using Npgsql;

namespace Tsukaba.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Board> Boards { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Post> Posts { get; set; }

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
            modelBuilder.Entity<Topic>().HasData(
                new Topic
                {
                    Id = 1,
                    Title = "Тохо-тред",
                    Message = @" Как минимум 74,67% девочек в Генсокё - украинки
                    На тох посмотришь, так они там все украинки.",
                    ImageUrl = "Baka.jpg",
                    Time = DateTime.Now,
                    BoardId = 1
                }
            );
        }
    }
}