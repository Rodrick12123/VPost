using Blog.Models.Domain;
using Blog.Models.ViewModels;
using Blog.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;

namespace Blog.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminBlogPostController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly IBlogPostRepository blogRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> loginManager;

        public AdminBlogPostController(ITagRepository tagRepository, IBlogPostRepository blogRepository,
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
            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(t => new SelectListItem { Text = t.DisplayName, Value = t.Id.ToString() })
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest request)
        {
            var blogPost = new BlogPost
            {
                Heading = request.Heading,
                PageTitle = request.PageTitle,
                Content = request.Content,
                ShortDescription = request.ShortDescription,
                FeaturedImageUrl = request.FeaturedImageUrl,
                UrlHandle = request.UrlHandle,
                PublishedDate = request.PublishedDate,
                Author = request.Author,
                Visible = request.Visible,
                Pending = false,
                Verified = true,

            };
            if (loginManager.IsSignedIn(User) == true){
                blogPost.UserId = Guid.Parse(userManager.GetUserId(User));

            }
            var selectedTags = new List<Tag>();
            foreach (var tagId in request.SelectedTags)
            {
                var Id = Guid.Parse(tagId);
                var tag = await tagRepository.GetAsync(Id);
                
                if(tag != null)
                {
                    selectedTags.Add(tag);
                }
            }
            blogPost.Tags = selectedTags;

            await blogRepository.AddAsync(blogPost);

            return RedirectToAction("Index", "Home");
        }
        //ToDo: Include pending status, filters, etc
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var allPosts = await blogRepository.GetAllAsync();
            return View(allPosts);
        }

        [HttpGet]
        //Id must be the same as the name of asp-route-id name "id"
        public async Task<IActionResult> Edit(Guid id)
        {

            var post = await blogRepository.GetAsync(id);
            var tags = await tagRepository.GetAllAsync();

            if (post != null)
            {
                var viewPost = new EditPostRequest
                {
                    Id = post.Id,
                    Heading = post.Heading,
                    PageTitle = post.PageTitle,
                    Content = post.Content,
                    Author = post.Author,
                    UrlHandle = post.UrlHandle,
                    FeaturedImageUrl = post.FeaturedImageUrl,
                    PublishedDate = post.PublishedDate,
                    Visible = post.Visible,
                    Pending = post.Pending,
                    ShortDescription = post.ShortDescription,
                    Tags = tags.Select(t => new SelectListItem { Text = t.Name, Value = t.Id.ToString() }),
                    SelectedTags = post.Tags.Select(t => t.Id.ToString()).ToArray()
                };
                if(post.Verified == true)
                {
                    viewPost.Verified = "true";
                }
                else
                {
                    viewPost.Verified = "false";
                }
                return View(viewPost);
            }

            return View(null);
            

        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPostRequest request)
        {

            //map domain model to request
            var domain = new BlogPost
            {
                Id = request.Id,
                Heading = request.Heading,
                Content = request.Content,
                PageTitle = request.PageTitle,
                Author = request.Author,
                ShortDescription = request.ShortDescription,
                FeaturedImageUrl = request.FeaturedImageUrl,
                PublishedDate = request.PublishedDate,
                UrlHandle = request.UrlHandle,
                Pending = false,
                Visible = request.Visible,

            };

            if(bool.TryParse(request.Verified, out bool parsed))
            {
                domain.Verified = parsed;
            }
            else
            {
                domain.Pending = true;
                ModelState.AddModelError("Verified", "Invalid type, input requires true or false.");
            }
            

            if (loginManager.IsSignedIn(User) == true)
            {
                domain.UserId = Guid.Parse(userManager.GetUserId(User));

            }

            //map tags to domain model
            var selectedTags = new List<Tag>();
            foreach(var selectedTag in request.SelectedTags)
            {
                if(Guid.TryParse(selectedTag, out var tag))
                {
                    var t = await tagRepository.GetAsync(tag);
                    if(t != null)
                    {
                        selectedTags.Add(t);
                    }
                }
            }
            domain.Tags = selectedTags;

            var updatedBlog = await blogRepository.UpdateAsync(domain);

            if(updatedBlog != null)
            {
                return RedirectToAction("Edit");
            }

            return RedirectToAction("Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditPostRequest request)
        {
            var deleted = await blogRepository.DeleteAsync(request.Id);
            if (deleted != null)
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new {id = request.Id});
        }
    }

}
