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
                return Created();
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
    }
}
