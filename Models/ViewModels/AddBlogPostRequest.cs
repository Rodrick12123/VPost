using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models.ViewModels
{
    public class AddBlogPostRequest
    {
        [Required(ErrorMessage = "The Heading field is required.")]
        public string Heading { get; set; }
        [Required(ErrorMessage = "The Page Title field is required.")]
        public string PageTitle { get; set; }
        [Required(ErrorMessage = "The Content field is required.")]
        public string Content { get; set; }
        public string ShortDescription { get; set; } = null;
        public string FeaturedImageUrl { get; set; } = null;
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "The Author field is required.")]
        public string Author { get; set; }
        public bool Visible { get; set; }

        public IEnumerable<SelectListItem> Tags { get; set; }
        public Guid? UserId { get; set; }

        public string[] SelectedTags { get; set; } = Array.Empty<string>();
    }
}
