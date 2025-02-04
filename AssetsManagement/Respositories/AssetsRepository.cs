using AssetsManagement.Contracts;
using AssetsManagement.DB;
using AssetsManagement.Models;
using MongoDB.Driver;

namespace AssetsManagement.Respositories
{
    public class AssetsRepository : IAssetsRepository
    {
        private DbContext _context;
        public AssetsRepository(DbContext context)
        {
            _context = context;
        }
        public async Task<List<Assets>> AddAssets(List<Assets> assets)
        {
            await _context.Assets.InsertManyAsync(assets);
            return assets;
        }
        public async Task<List<Assets>> GetAssets()
        {
            var filter = Builders<Assets>.Filter.Empty;
            var allAssets = await _context.Assets.Find(filter).ToListAsync();
            return allAssets;
        }
    }
}
