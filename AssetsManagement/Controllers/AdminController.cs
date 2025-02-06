using AssetsManagement.Data;
using AssetsManagement.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace AssetsManagement.Controllers
{
    [ApiController]
    [Route("/api/admin")]
    public class AdminController : Controller
    {
        private readonly IInputData _textInputData;
        private readonly IInputData _jsonInputData;
        public AdminController([FromKeyedServices("text")] IInputData textInputData, [FromKeyedServices("json")] IInputData jsonInputData)
        {
            _textInputData = textInputData;
            _jsonInputData = jsonInputData;
        }
        /// <summary>
        /// Add bulk data to the database through text file.
        /// </summary>
        /// <returns> </returns>

        [HttpPost("text")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddTextData()
        {
            Console.WriteLine("Adding text data...");
            bool status = await _textInputData.InsertInputData();
            if (status)
                return Created("", "Resource created successfully");

            else
                return StatusCode(500);
        }
        /// <summary>
        /// Add bulk data to the database through json file.
        /// </summary>
        /// <returns></returns>
        [HttpPost("json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddJsonData()
        {
            Console.WriteLine("Adding json data...");
            bool status = await _jsonInputData.InsertInputData();
            if (status)
                return Created("", "Resource created successfully");

            else
                return StatusCode(500);
        }
    }
}
