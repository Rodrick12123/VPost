using Azure.Core;
using Blog.Data;
using Blog.Models.Domain;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Blog.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BlogDbContext dbContext;

        public BlogPostRepository(BlogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<BlogPost>?> GetAllVerifiedAsync()
        {
            var verifiedPost = await dbContext.BlogPosts.Include(t => t.Tags).Where(p => p.Verified == true).ToListAsync();
            return verifiedPost;
        }

        public async Task<IEnumerable<BlogPost>?> GetAllPendingAsync()
        {
            var pendingPost = await dbContext.BlogPosts.Include(t => t.Tags).Where(p => p.Pending == true).ToListAsync();
            return pendingPost;
        }

        public async Task<BlogPost> AddAsync(BlogPost post)
        {
            await dbContext.AddAsync(post);
            await dbContext.SaveChangesAsync();
            return post;
            
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var blog = await dbContext.BlogPosts.FindAsync(id);

            if (blog != null)
            {
                dbContext.BlogPosts.Remove(blog);
                await dbContext.SaveChangesAsync();
                return blog;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            //blog posts includes relation db
            var allBlogs = await dbContext.BlogPosts.Include(t => t.Tags).ToListAsync();
           
            return allBlogs;
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            var post = await dbContext.BlogPosts.Include(t => t.Tags).FirstOrDefaultAsync(p => p.Id == id);
            return post;
        }

        public async Task<BlogPost?> GetUrlHandelAsync(string urlHandel)
        {

            var blog = await dbContext.BlogPosts.Include(t => t.Tags).FirstOrDefaultAsync(b => b.UrlHandle == urlHandel);
            return blog;
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost post)
        {
            var existingBlog = await dbContext.BlogPosts.Include(t => t.Tags).FirstOrDefaultAsync(p => p.Id == post.Id);
            if(existingBlog != null)
            {
                existingBlog.Id = post.Id;
                existingBlog.Heading = post.Heading;
                existingBlog.Content = post.Content;
                existingBlog.PageTitle = post.PageTitle;
                existingBlog.Author = post.Author;
                existingBlog.ShortDescription = post.ShortDescription;
                existingBlog.FeaturedImageUrl = post.FeaturedImageUrl;
                existingBlog.PublishedDate = post.PublishedDate;
                existingBlog.UrlHandle = post.UrlHandle;
                existingBlog.Visible = post.Visible;
                existingBlog.Tags = post.Tags;
                existingBlog.Verified = post.Verified;
                existingBlog.Pending = post.Pending;
                await dbContext.SaveChangesAsync();
                return existingBlog;
            }
            return null;
        }
    }
}
