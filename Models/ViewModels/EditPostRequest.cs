using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models.ViewModels
{
    public class EditPostRequest
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
        public DateTime PublishedDate { get; set; } = DateTime.Now;
        [Required]
        public string Author { get; set; }
        public bool Visible { get; set; }
        public string Verified { get; set; }
        public bool Pending { get; set; }

        public IEnumerable<SelectListItem> Tags { get; set; }

        public string[] SelectedTags { get; set; } = Array.Empty<string>();
    }
}
