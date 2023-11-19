using Blog.Data;
using Blog.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog.Repositories
{
    public class BlogPostComment : IBlogPostCommentRepository
    {
        private readonly BlogDbContext blogContext;

        public BlogPostComment(BlogDbContext blogContext)
        {
            this.blogContext = blogContext;
        }
        public async Task<PostComment> AddAsync(PostComment blogComment)
        {
            await blogContext.PostComments.AddAsync(blogComment);
            await blogContext.SaveChangesAsync();
            return blogComment;
        }

        public async Task<IEnumerable<PostComment>> GetAllCommentsByPostId(Guid postId)
        {
            return await blogContext.PostComments.Where(p => p.BlogPostId == postId).ToListAsync();
        }
    }
}
