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
        
        [HttpPost]
        public async Task<IActionResult> AddData()
        {
            Console.WriteLine("Adding data...");
            await _inputData.InsertInputData();
            return Ok(new { message = "Data added successfully!" });
        }
    }
}
