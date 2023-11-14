using Blog.Models.Domain;

namespace Blog.Repositories
{
    public interface ITagRepository
    {
        //Method Definitions
        Task<IEnumerable<Tag>> GetAllAsync();
        
        Task<Tag?> GetAsync(Guid id);

        Task<Tag> AddAsync(Tag tag);

        Task<Tag?> UpdateAsync(Tag tag);

        Task<Tag?> DeleteAsync(Guid tag);
    }
}
