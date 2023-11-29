namespace Blog.Models.Domain
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }

        //define sql relationship
        public ICollection<BlogPost> BlogPosts { get; set; }
        public Guid UserId { get; set; }
        public UserDomain User { get; set; } = null!;


    }
}
