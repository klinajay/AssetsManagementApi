using AssetsManagement.Models;
using AssetsManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace AssetsManagement.Controllers
{
    [ApiController]
    [Route("/api/assets")]
    public class AssetsController : Controller
    {
        private readonly AssetsService _assetservice;
        public AssetsController(AssetsService assetservice)
        {
            _assetservice = assetservice;
        }
        [HttpPost]
        public async Task<IActionResult> AddAssets([FromBody] List<Assets> assets)
        {

            await _assetservice.AddAssets(assets);
            return Ok();
        }
    }
}
