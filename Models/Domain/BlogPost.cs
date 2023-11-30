using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace Blog.Models.Domain
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        
        public string Content { get; set; }
        public string? ShortDescription { get; set; }
        public string? FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        
        public string Author { get; set; }
        public bool Visible { get; set; }
        public bool Pending { get; set; } = true;
        public bool Verified { get; set; } = false;

        public Guid? UserId { get; set; }
        public UserDomain User { get; set; } = null!;
        public Guid? ReplyToId { get; set; }


        //initiailize sql relationship
        public ICollection<Tag> Tags { get; set; }

        public ICollection<BlogPostLike>  Likes { get; set; }
        public ICollection<PostComment> Comments { get; set; }
        public ICollection<BlogPost> BlogReplies { get; set; }

    }
}
