using AssetsManagement.Respositories;
using AssetsManagement.Contracts;
using AssetsManagement.Models;

namespace AssetsManagement.Services
{
    public class AssetsService
    {
        private readonly IAssetsRepository _assetsRepository;
        public AssetsService(IAssetsRepository assetsRepository)
        {
            _assetsRepository = assetsRepository;
        }
        public async Task<List<Assets>> AddAssets(List<Assets> assets)
        {
            return await _assetsRepository.AddAssets(assets);
        }
        public async Task<List<Assets>> GetAssets()
        {
            return await _assetsRepository.GetAssets();
        }
    }
}
