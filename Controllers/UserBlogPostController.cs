using Blog.Models.Domain;
using Blog.Models.ViewModels;
using Blog.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog.Controllers
{
    [Authorize]
    public class UserBlogPostController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly IBlogPostRepository blogRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> loginManager;

        public UserBlogPostController(ITagRepository tagRepository, IBlogPostRepository blogRepository,
            UserManager<IdentityUser> userManager, SignInManager<IdentityUser> loginManager)
        {
            this.tagRepository = tagRepository;
            this.blogRepository = blogRepository;
            this.userManager = userManager;
            this.loginManager = loginManager;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var tags = await tagRepository.GetAllAsync();
            var model = new AddBlogPostRequestUser
            {
                Tags = tags.Select(t => new SelectListItem { Text = t.DisplayName, Value = t.Id.ToString() })
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequestUser request)
        {
            // Generate a unique identifier (you can use a GUID for simplicity)
            string urlId = Guid.NewGuid().ToString("N");

            // Combine the title and unique identifier and create a URL-friendly string
            string urlHandle = $"{request.Author}-{urlId}".ToLower().Replace(" ", "-");

            var blogPost = new BlogPost
            {
                Heading = request.Heading,
                PageTitle = request.PageTitle,
                Content = request.Content,
                ShortDescription = request.ShortDescription,
                FeaturedImageUrl = request.FeaturedImageUrl,
                UrlHandle = urlHandle,
                PublishedDate = DateTime.Now,
                Author = request.Author,
                Visible = request.Visible,

            };
            if (loginManager.IsSignedIn(User) == true)
            {
                blogPost.UserId = Guid.Parse(userManager.GetUserId(User));

            }
            var selectedTags = new List<Tag>();
            foreach (var tagId in request.SelectedTags)
            {
                var Id = Guid.Parse(tagId);
                var tag = await tagRepository.GetAsync(Id);

                if (tag != null)
                {
                    selectedTags.Add(tag);
                }
            }
            blogPost.Tags = selectedTags;

            await blogRepository.AddAsync(blogPost);
            TempData["Message"] = "Your post request has been saved! The site admin will review it shortly.";

            return RedirectToAction("Index", "Home");
        }
    }
}
