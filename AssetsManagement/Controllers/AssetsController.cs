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
        /// <summary>
        /// Add assets information.
        /// </summary>
        /// <param name="assets"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAssets([FromBody] List<Assets> assets)
        {

            var result = await _assetservice.AddAssets(assets);
            if (result.Count == 0)
                return StatusCode(500);
            else
                return Created("", "Resource created successfully");
        }
        /// <summary>
        /// Get all assets.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAssets()
        {

            var assets = await _assetservice.GetAssets();
            if (assets.Count > 0)
                return Ok(assets);
            else if (assets.Count == 0)
                return NoContent();
            else
                return StatusCode(500);
        }
    }
}
