using Blog.Models.Domain;
using Blog.Models.ViewModels;
using Blog.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    //ToDo:Fix like button not working
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
        //ToDO: Fix Remove Like option
        [HttpPost]
        [Route("Remove")]
        public async Task<IActionResult> RemoveLike([FromBody] AddLikeRequest likeRequest)
        {

            var model = new BlogPostLike
            {
                BlogPostId = likeRequest.BlogPostId,
                UserId = likeRequest.UserId
            };
            await likeRepo.RemoveLike(model);
            //ToDO: Message if like was removed
            return Ok();

        }

        [HttpGet]
        [Route("PostId:Guid")]
        public async Task<IActionResult> GetTotalLikes([FromRoute]Guid postId)
        {
            var totalAmountOfLikes = await likeRepo.GetLikeTotal(postId);
            return Ok(totalAmountOfLikes);
        }
    }
}
