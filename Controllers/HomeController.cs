using Blog.Models;
using Blog.Models.ViewModels;
using Blog.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Blog.Controllers
{
    //Ideal: Home page should include other posts,
    //such as Music, stories, adds, etc
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITagRepository tagRepo;
        private readonly IBlogPostRepository blogRepo;

        public HomeController(ILogger<HomeController> logger,ITagRepository tagRepo, IBlogPostRepository blogRepo)
        {
            _logger = logger;
            this.tagRepo = tagRepo;
            this.blogRepo = blogRepo;
        }

        public async Task<IActionResult> Index()
        {
            //Get all of the blog posts
            var post = await blogRepo.GetAllVerifiedAsync();
            //Get all of the tags
            var tags = await tagRepo.GetAllAsync();
            //Combine two view models in HomeViewModel
            var Model = new HomeViewModel
            {
                blogPosts = post,
                Tags = tags
            };

            return View(Model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}