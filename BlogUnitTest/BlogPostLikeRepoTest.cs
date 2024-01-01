using Blog.Data;
using Blog.Models.Domain;
using Blog.Repositories;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;

namespace BlogUnitTest
{
    [TestClass]
    public class BlogPostLikeRepoTest
    {
        [TestMethod]
        public async Task TestAddLikeAsync()
        {
            // Set up the DBContext repository and like
            var dbContext = GetDbContext();
            var repository = new BlogPostLikeRepository(dbContext);
            var blogLike = new BlogPostLike 
                            { 
                                Id = Guid.NewGuid(),
                                BlogPostId = Guid.NewGuid(),
                                UserId = Guid.NewGuid(),
                            };

            // Result
            var result = await repository.AddLike(blogLike);

            // Assertions
            Assert.IsNotNull(result);
            Assert.AreEqual(blogLike.BlogPostId, result.BlogPostId);
            Assert.AreEqual(blogLike.UserId, result.UserId);
            // Include more assertions if needed
        }

        [TestMethod]
        public async Task TestRemoveLikeAsync()
        {
            // Arrange
            var dbContext = GetDbContext();
            var repository = new BlogPostLikeRepository(dbContext);
            var blogLike = new BlogPostLike
            {
                Id = Guid.NewGuid(),
                BlogPostId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
            };
            // Result
            var result = await repository.RemoveLike(blogLike);

            // Assert
            Assert.IsNull(result); 
            // Additional assertions if needed
        }

        [TestMethod]
        public async Task TestGetAllBlogPostLikesAsync()
        {
            // Arrange
            var dbContext = GetDbContext();
            var repository = new BlogPostLikeRepository(dbContext);
            var postId = Guid.NewGuid();

            // Result
            var result = await repository.GetAllBlogPostLikes(postId);

            // Assert
            Assert.IsNotNull(result);
            // Add more assertions if needed
        }

        [TestMethod]
        public async Task TestGetLikeTotalAsync()
        {
            // Arrange
            var dbContext = GetDbContext();
            var repository = new BlogPostLikeRepository(dbContext);
            var postId = Guid.NewGuid(); 

            // Results
            var result = await repository.GetLikeTotal(postId);

            // Assert
            Assert.AreEqual(0, result);
            // Additional assertions if needed

            var blogLike = new BlogPostLike
            {
                Id = Guid.NewGuid(),
                BlogPostId = postId,
                UserId = Guid.NewGuid(),
            };

            // Result when like added
            await repository.AddLike(blogLike);

            result = await repository.GetLikeTotal(postId);

            Assert.AreEqual(1, result);

        }

        private BlogDbContext GetDbContext()
        {
            // Set up db build options
            var options = new DbContextOptionsBuilder<BlogDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new BlogDbContext(options);

            // Seed dbContext if needed

            return dbContext;
        }
    }
}
