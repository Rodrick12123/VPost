
using Blog.Data;
using Blog.Models.Domain;
using Blog.Repositories;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;



namespace BlogUnitTest
{
    [TestClass]
    public class BlogPostCommentRepoTest
    {
        
        [TestMethod] 
        public async Task TestBlogPostComment_AddAsync_PostComment()
        {
            //set db context
            var fakeDbContext = GetDbContext();
            //init the db set
            //var fakeDbSet = A.Fake<DbSet<PostComment>>();
            //generate a fake call to the fake db context
            //A.CallTo(() => fakeDbContext.PostComments).Returns(fakeDbSet);
            //init repository with the fake dbcontext
            var fakeRepo = new BlogPostComment(fakeDbContext);

            //create the blog comment for testing
            PostComment fakeComment = new PostComment{
                CommentDate = DateTime.Now,
                Description = "Some comment",
                BlogPostId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                
            };

            var result = await fakeRepo.AddAsync(fakeComment);

            //assertions ideally 3-5 
            Console.WriteLine($"myVariable: {fakeDbContext.PostComments.First(d => d.Description == "Some comment").Id}");
            Assert.IsNotNull(result);
            Assert.AreEqual(result, fakeComment);
            Assert.IsNotNull(fakeDbContext.PostComments.Where(d => d.Description == "Some comment"));

            var commentsInDb = fakeDbContext.PostComments.ToList();
            Assert.IsNotNull(commentsInDb);
            Assert.IsTrue(commentsInDb.Contains(fakeComment));

        }

        [TestMethod]
        public async Task TestBlogPostComment_GetAllCommentsByPostId_PostComment()
        {
            //set db context
            var fakeDbContext = GetDbContext();
            
            var fakeRepo = new BlogPostComment(fakeDbContext);

            //init the guid for the postId
            var postId = Guid.NewGuid();

            //create the blog comment for testing
            PostComment fakeComment = new PostComment
            {
                CommentDate = DateTime.Now,
                Description = "Some comment2",
                BlogPostId = postId,
                UserId = Guid.NewGuid(),

            };

            var addComment = await fakeRepo.AddAsync(fakeComment);
            var result = await fakeRepo.GetAllCommentsByPostId(postId);

            //assertions ideally 3-5 
            Console.WriteLine($"myVariable: {fakeDbContext.PostComments.First(d => d.Description == "Some comment2").Id}");
            Assert.IsNotNull(result);
            Assert.AreEqual(result.FirstOrDefault(), fakeComment);
            Assert.IsNotNull(fakeDbContext.PostComments.Where(d => d.BlogPostId == postId));

            var commentsInResult = result.ToList();
            Assert.IsNotNull(commentsInResult);
            Assert.IsTrue(commentsInResult.Contains(fakeComment));

        }

        private BlogDbContext GetDbContext()
        {
            //Set up db build options
            var options = new DbContextOptionsBuilder<BlogDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new BlogDbContext(options);
            //Add migrations
            
            //dbContext.Database.EnsureCreated();
            //Seed dbContext
            return dbContext;
        }

    }
}