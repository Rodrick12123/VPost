using Blog.Models.Domain;
using Blog.Models.ViewModels;

namespace Blog.Repositories
{
    public interface IBlogPostLikeRepository
    {
        Task<int> GetLikeTotal(Guid postId);
        Task<BlogPostLike> AddLike(BlogPostLike blogLike);

    }
}
