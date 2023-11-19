using Blog.Data;
using Blog.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly BlogDbContext dbContext;

        public BlogPostLikeRepository(BlogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<BlogPostLike> AddLike(BlogPostLike blogLike)
        {
            await dbContext.BlogPostLikes.AddAsync(blogLike);
            await dbContext.SaveChangesAsync();
            return blogLike;
        }

        public async Task<IEnumerable<BlogPostLike>> GetAllBlogPostLikes(Guid postId)
        {
            return await dbContext.BlogPostLikes.Where(p => p.BlogPostId == postId).ToListAsync();
        }

        public async Task<int> GetLikeTotal(Guid postId)
        {
            var totalLikes = await dbContext.BlogPostLikes.CountAsync(b => b.BlogPostId == postId);
            return totalLikes;
        }

        
    }
}
