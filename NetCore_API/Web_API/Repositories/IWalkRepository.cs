using Web_API.Models.Domain;

namespace Web_API.Repositories
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetAllAsync();
        Task<Walk?> GetByIdAsync(Guid id);
        Task<Walk> CreateAsync(Walk walk);
        Task<Walk> UpdateAsync(Guid id, Walk region);
        Task<Walk> DeleteAsync(Guid id);
    }
}
