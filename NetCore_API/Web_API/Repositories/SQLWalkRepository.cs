using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Web_API.Data;
using Web_API.Models.Domain;

namespace Web_API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext _dbContext;
        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await _dbContext.Walks
                .Include(x => x.Difficulty)
                .Include("Region")
                .ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Walks.FindAsync(id);
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await _dbContext.Walks.AddAsync(walk);
            await _dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
            var existingWalk = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null)
                return null;

            _dbContext.Walks.Remove(existingWalk);
            await _dbContext.SaveChangesAsync();
            return existingWalk;
        }

        public async Task<Walk> UpdateAsync(Guid id, Walk region)
        {
            var existingWalk =  await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if(existingWalk == null)
                return null;

            //Map DTO to Domain Model
            existingWalk.Name = region.Name;
            existingWalk.Description = region.Description;
            existingWalk.LengthInKm = region.LengthInKm;
            existingWalk.WalkImageUrl = region.WalkImageUrl;
            existingWalk.RegionId = region.RegionId;
            existingWalk.DifficultyId = region.DifficultyId;

            await _dbContext.SaveChangesAsync();
            return existingWalk;
        }
    }
}
