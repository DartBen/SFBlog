using BlogAppLib.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogAppLib
{
    public class BlogBD : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public BlogBD(DbContextOptions<BlogBD> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}