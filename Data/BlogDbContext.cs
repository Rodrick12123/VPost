using Blog.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {

        }

        //Create the DB tables BlopPosts and Tags
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public DbSet<BlogPostLike> BlogPostLikes { get; set; }
        public DbSet<PostComment> PostComments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            


        }
    }
}
