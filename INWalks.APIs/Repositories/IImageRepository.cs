using INWalks.APIs.Models.Domain;
using System.Net;

namespace INWalks.APIs.Repositories
{
    public interface IImageRepository
    {
        Task<Image> UploadAsync(Image image);
    }
}
