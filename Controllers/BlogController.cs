﻿using Azure.Core;
using Blog.Models.Domain;
using Blog.Models.ViewModels;
using Blog.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogPostRepository blogRepo;
        private readonly IBlogPostLikeRepository likeRepo;
        private readonly SignInManager<IdentityUser> loginManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IBlogPostCommentRepository commentRepo;

        public BlogController(IBlogPostRepository blogRepo, IBlogPostLikeRepository likeRepo,
            SignInManager<IdentityUser> loginManager, UserManager<IdentityUser> userManager, IBlogPostCommentRepository commentRepo)
        {
            this.blogRepo = blogRepo;
            this.likeRepo = likeRepo;
            this.loginManager = loginManager;
            this.userManager = userManager;
            this.commentRepo = commentRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandel)
        {
            
            var blog = await blogRepo.GetUrlHandelAsync(urlHandel);
            var viewBlogWithTotalLikes = new BlogDetails();
            var liked = false;
            if (blog != null)
            {
                if (loginManager.IsSignedIn(User))
                {
                    var listOfLikes = await likeRepo.GetAllBlogPostLikes(blog.Id);
                    var userId = userManager.GetUserId(User);
                    if(userId != null)
                    {
                        var userLike = listOfLikes.FirstOrDefault(u => u.UserId == Guid.Parse(userId));
                        if (userLike != null)
                        {
                            liked = true;
                        }
                    }
                }
                var comments = await commentRepo.GetAllCommentsByPostId(blog.Id);
                var viewComments = new List<CommentView>();
                foreach (var comment in comments)
                {
                    viewComments.Add(new CommentView
                    {
                        Description = comment.Description,
                        CommentDate = comment.CommentDate,
                        UserName = (await userManager.FindByIdAsync(comment.UserId.ToString())).UserName
                    });
                }
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
                    Tags = blog.Tags,
                    Liked = liked,
                    Comments = viewComments
                };
            }
            
            return View(viewBlogWithTotalLikes);
        }

        [HttpPost]
        public async Task<IActionResult> Index(BlogDetails blogDetails)
        {
            if (loginManager.IsSignedIn(User))
            {
                var comment = new PostComment
                {
                    //Entity Framework Will generate the ID
                    BlogPostId = blogDetails.Id,
                    Description = blogDetails.CommentDescription,
                    UserId = Guid.Parse(userManager.GetUserId(User)),
                    CommentDate = DateTime.Now

                };
                await commentRepo.AddAsync(comment);
                return RedirectToAction("Index", "Home", new {handel = blogDetails.UrlHandle});
            }

            return Forbid();
        }
    }
}
