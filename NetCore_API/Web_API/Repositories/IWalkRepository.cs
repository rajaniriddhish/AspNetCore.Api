﻿using Web_API.Models.Domain;

namespace Web_API.Repositories
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null,
            bool isAscending = true, int pageNumber = 1, int pageSize = 1000);
        Task<Walk?> GetByIdAsync(Guid id);
        Task<Walk> CreateAsync(Walk walk);
        Task<Walk> UpdateAsync(Guid id, Walk region);
        Task<Walk> DeleteAsync(Guid id);
    }
}
