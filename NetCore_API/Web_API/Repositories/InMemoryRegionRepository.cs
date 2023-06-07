using Web_API.Models.Domain;

namespace Web_API.Repositories
{
    //Create for test
    public class InMemoryRegionRepository : IRegionRepository
    {
        public InMemoryRegionRepository() { }

        public Task<Region> CreateAsync(Region region)
        {
            throw new NotImplementedException();
        }

        public Task<Region> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return new List<Region>()
            {
                    new Region {
                        Id = Guid.NewGuid(),
                        Name = "Auckland Region",
                        Code = "AKL",
                        RegionImageUrl = "https://images.freeimages.com/variants/i6YmGLtHwW66p5byh1JRqUdX/f4a36f6589a0e50e702740b15352bc00e4bfaf6f58bd4db850e167794d05993d"
                    },
                    new Region {
                        Id = Guid.NewGuid(),
                        Name = "Wellington Region",
                        Code = "WLG",
                        RegionImageUrl = "https://images.freeimages.com/variants/i6YmGLtHwW66p5byh1JRqUdX/f4a36f6589a0e50e702740b15352bc00e4bfaf6f58bd4db850e167794d05993d"
                    }
            };
        }

        public Task<Region?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Region> UpdateAsync(Guid id, Region region)
        {
            throw new NotImplementedException();
        }
    }
}
