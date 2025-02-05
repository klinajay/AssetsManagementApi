using AssetsManagement.Data;
using AssetsManagement.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AssetsManagement.Controllers
{
    [ApiController]
    [Route("/api/admin")]
    public class AdminController : Controller
    {
        public IInputData _inputData;
        public AdminController(IInputData context)
        {
            _inputData = context;
        }
        /// <summary>
        /// Add bulk data to the database.
        /// </summary>
        /// <returns> </returns>
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddData()
        {
            Console.WriteLine("Adding data...");
            bool status = await _inputData.InsertInputData();
            if (status)
                return Created();
              
            else
                return StatusCode(500);
        }
    }
}
