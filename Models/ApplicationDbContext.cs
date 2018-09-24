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
            
        }
    }
}