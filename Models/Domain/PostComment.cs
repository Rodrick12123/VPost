namespace Blog.Models.Domain
{
    public class PostComment
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        //TODO: Consider making the BlogPostId nullable or initially null 
        //and link this comment to another post type
        public Guid BlogPostId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CommentDate { get; set; }

    }
}
