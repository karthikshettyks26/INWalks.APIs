using INWalks.APIs.Models.Domain;
using System.Runtime.InteropServices;

namespace INWalks.APIs.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync();
    }
}
