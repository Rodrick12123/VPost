using Blog.Models.Domain;

namespace Blog.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<BlogPost> blogPosts { get; set; }

        public IEnumerable<Tag> Tags { get; set; }
    }
}
