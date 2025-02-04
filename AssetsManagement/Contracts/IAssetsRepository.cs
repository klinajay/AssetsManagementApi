using AssetsManagement.Models;

namespace AssetsManagement.Contracts
{
    public interface IAssetsRepository
    {
        public Task<List<Assets>> AddAssets(List<Assets> machines);
        public Task<List<Assets>> GetAssets();
    }
}
