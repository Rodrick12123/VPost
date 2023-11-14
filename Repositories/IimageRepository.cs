namespace Blog.Repositories
{
    public interface IimageRepository
    {

        Task<string> UploadAsync(IFormFile file);
    }
}
