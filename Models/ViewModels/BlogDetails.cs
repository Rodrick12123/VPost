using Blog.Models.Domain;

namespace Blog.Models.ViewModels
{
    public class BlogDetails
    {
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }
        public string CommentDescription { get; set; }
        public IEnumerable<CommentView> Comments { get; set; }

        //initiailize sql relationship
        public ICollection<Tag> Tags { get; set; }

        public bool Liked { get; set; }
        public int LikeTotal { get; set; }
    }
}
