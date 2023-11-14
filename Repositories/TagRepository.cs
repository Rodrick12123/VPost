using Azure.Core;
using Blog.Data;
using Blog.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BlogDbContext blogDbContext;
        public TagRepository(BlogDbContext blogDbContext)
        {
            this.blogDbContext = blogDbContext;
            
        }
        public async Task<Tag> AddAsync(Tag tag)
        {
            
            //add the model instance to the tag table in the DB
            await blogDbContext.Tags.AddAsync(tag);

            //save changes to db
            await blogDbContext.SaveChangesAsync();

            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var deleteTag = await blogDbContext.Tags.FindAsync(id);
            if (deleteTag != null)
            {
                blogDbContext.Remove(deleteTag);
                await blogDbContext.SaveChangesAsync();

                return deleteTag;
            }
            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            var tags = await blogDbContext.Tags.ToListAsync();
            return tags;
        }

        public Task<Tag?> GetAsync(Guid id)
        {
            return blogDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var exsistingTag = await blogDbContext.Tags.FindAsync(tag.Id);
            if (exsistingTag != null)
            {
                exsistingTag.Name = tag.Name;
                exsistingTag.DisplayName = tag.DisplayName;

                await blogDbContext.SaveChangesAsync();
                return exsistingTag;
            }
            return null;

        }
    }
}
