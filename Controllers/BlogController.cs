using Azure.Core;
using Blog.Models.ViewModels;
using Blog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogPostRepository blogRepo;
        private readonly IBlogPostLikeRepository likeRepo;

        public BlogController(IBlogPostRepository blogRepo, IBlogPostLikeRepository likeRepo)
        {
            this.blogRepo = blogRepo;
            this.likeRepo = likeRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandel)
        {
            var blog = await blogRepo.GetUrlHandelAsync(urlHandel);
            var viewBlogWithTotalLikes = new BlogDetails();
            if (blog != null)
            {
                var totalLikes = await likeRepo.GetLikeTotal(blog.Id);
                viewBlogWithTotalLikes = new BlogDetails
                {
                    Id = blog.Id,
                    Heading = blog.Heading,
                    Content = blog.Content,
                    PageTitle = blog.PageTitle,
                    Author = blog.Author,
                    ShortDescription = blog.ShortDescription,
                    FeaturedImageUrl = blog.FeaturedImageUrl,
                    PublishedDate = blog.PublishedDate,
                    UrlHandle = blog.UrlHandle,
                    Visible = blog.Visible,
                    LikeTotal = totalLikes,
                    Tags = blog.Tags
                };
            }
            
            return View(viewBlogWithTotalLikes);
        }
    }
}
