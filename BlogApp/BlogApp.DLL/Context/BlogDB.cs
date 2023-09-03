using BlogApp.DLL.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.DLL.Context
{
    public class BlogDB : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Role> Roles { get; set; }

        public BlogDB(DbContextOptions<BlogDB> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=BlogAppDB.db");
        }
    }
}