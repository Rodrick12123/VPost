using Blog.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models.ViewModels
{
    public class BlogResponseDetails
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "The Heading field is required.")]
        public string Heading { get; set; }
        [Required(ErrorMessage = "The Page Title field is required.")]
        public string PageTitle { get; set; }
        [Required(ErrorMessage = "The Content field is required.")]
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        [Required(ErrorMessage = "Author required.")]
        public string Author { get; set; }
        public bool Visible { get; set; }

        //initiailize sql relationship
        public ICollection<Tag> Tags { get; set; }

        public Guid? ReplyToId { get; set; }
    }
}
