    using NZWalks.Api.Models.Domain;

namespace NZWalks.Repositories
{
    public interface IRegionRepository
    {
        public Task<List<Region>> GetAllAsync();
        public Task<Region?> GetByIdAsync(Guid id);
        public Task<Region> CreateAsync(Region region);
        public Task<Region?> UpdateAsync(Guid id, Region region);
        public Task<Region?> DeleteAsync(Guid id);

    }
}
