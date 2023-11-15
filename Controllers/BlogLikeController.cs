using Blog.Models.Domain;
using Blog.Models.ViewModels;
using Blog.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogLikeController : ControllerBase
    {
        private readonly IBlogPostLikeRepository likeRepo;

        public BlogLikeController(IBlogPostLikeRepository likeRepo)
        {
            this.likeRepo = likeRepo;
        }
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddLike([FromBody] AddLikeRequest likeRequest)
        {
            
            var model = new BlogPostLike
            {
                BlogPostId = likeRequest.BlogPostId,
                UserId = likeRequest.UserId
            };
            await likeRepo.AddLike(model);
            return Ok();
            
        }
    }
}
