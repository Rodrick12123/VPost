using Blog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogPostRepository blogRepo;

        public BlogController(IBlogPostRepository blogRepo)
        {
            this.blogRepo = blogRepo;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string urlHandel)
        {
            var blog = await blogRepo.GetUrlHandelAsync(urlHandel);
            
            return View(blog);
        }
    }
}
