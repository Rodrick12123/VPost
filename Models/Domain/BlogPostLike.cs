namespace Blog.Models.Domain
{
    public class BlogPostLike
    {
        public Guid Id { get; set; }
        public Guid BlogPostId { get; set; }
        public Guid UserId { get; set; }
        public UserDomain User { get; set; } = null!;
    }
}
