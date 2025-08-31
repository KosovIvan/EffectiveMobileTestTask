using EffectiveMobileTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace EffectiveMobileTest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private static LocationTree _locationTree = new();

        [HttpPost]
        public async Task<IActionResult> LoadData(IFormFile file)
        {

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetPlatforms([FromQuery] string platforms)
        {

            return Ok();
        }
    }
}
