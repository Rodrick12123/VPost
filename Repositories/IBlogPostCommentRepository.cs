using Blog.Models.Domain;

namespace Blog.Repositories
{
    public interface IBlogPostCommentRepository
    {
        Task<PostComment> AddAsync(PostComment  blogComment);
        Task<IEnumerable<PostComment>> GetAllCommentsByPostId(Guid postId);
    }
}
