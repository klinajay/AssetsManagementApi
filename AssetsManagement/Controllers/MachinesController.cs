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
        /// Add data of Machines.
        /// </summary>
        /// <param name="machines"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddMachine([FromBody] List<Machines> machines)
        {

            await _machineService.AddMachine(machines);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetMachines()
        {

            await _machineService.GetMachines();
            return Ok();
        }
    }
}
