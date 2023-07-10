using System.Net;
using Web_API.Models.Domain;

namespace Web_API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
