using System.Xml.Linq;
using AssetsManagement.Contracts;
using AssetsManagement.DB;
using AssetsManagement.Models;
using AssetsManagement.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace AssetsManagement.Controllers
{
    [ApiController]
    [Route("/api/machines")]
    public class MachinesController : Controller
    {
        private readonly MachinesService _machineService;
        private readonly AssetsService _assetsService;
        public MachinesController(MachinesService machineService, AssetsService assetsService)
        {
            _machineService = machineService;
            _assetsService = assetsService;
        }
        /// <summary>
        /// Add machines information.
        /// </summary>
        /// <param name="machines"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddMachine([FromBody] List<Machines> machines)
        {

            var result = await _machineService.AddMachine(machines);
            if (result.Count == 0)
                return StatusCode(500);
            else
                return Created("", "Resource created successfully");
        }
        /// <summary>
        /// Get all machines
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMachines()
        {

            var machines = await _machineService.GetMachines();
            if (machines.Count > 0)
                return Ok(machines);
            else if (machines.Count == 0)
                return NoContent();
            else
                return StatusCode(500);
        }

        [HttpGet("{name}/assets")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType (StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAssetsUsedByMachine(string name)
        {
            try
            {
                Console.WriteLine("Inside GetAssetsUsedByMachine");
                var assets = await _machineService.GetAssetsUsedByMachines(name);
                if (assets == null || assets.Count == 0)
                {
                    return NoContent();
                }
                return Ok(assets);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                
                Console.Error.WriteLine($"Error in GetAssetsUsedByMachine: {ex.Message}");
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }
        /// <summary>
        /// Get machines that are using only the latest assets.
        /// </summary>
        /// <returns>List of machines using the latest assets</returns>
        [HttpGet("latest-assets")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMachinesUsingLatestAssets()
        {
            try
            {
                var machines = await _machineService.GetMachinesUsingLatestAssets();

                if (machines == null || machines.Count == 0)
                {
                    return NoContent(); 
                }

                return Ok(machines); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        

    }
}
