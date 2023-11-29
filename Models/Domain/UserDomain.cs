using Microsoft.AspNetCore.Identity;

namespace Blog.Models.Domain
{
    public class UserDomain: IdentityUser
    {
        public ICollection<BlogPost> BlogPosts { get; set; }
        public ICollection<PostComment> Comments { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
