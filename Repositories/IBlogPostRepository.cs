using Blog.Models.Domain;

namespace Blog.Repositories
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<IEnumerable<BlogPost>?> GetAllPendingAsync();
        Task<IEnumerable<BlogPost>?> GetAllVerifiedAsync();

        Task<BlogPost?> GetAsync(Guid id);

        Task<BlogPost> AddAsync(BlogPost post);

        Task<BlogPost?> UpdateAsync(BlogPost post);

        Task<BlogPost?> DeleteAsync(Guid id);
        Task<BlogPost?> GetUrlHandelAsync(string urlHandel);



    }
}
