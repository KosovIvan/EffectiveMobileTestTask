using EffectiveMobileTest.Models;
using EffectiveMobileTest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Text;
using System.Text.RegularExpressions;

namespace EffectiveMobileTest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly LocationTreeService _locationTreeService;
        public PlatformController(LocationTreeService locationTreeService)
        {
            _locationTreeService = locationTreeService;
        }

        [HttpPost]
        public async Task<IActionResult> LoadData(IFormFile? file)
        {
            if (file == null || file.Length == 0) return BadRequest("Файл пуст или отсутствует");
            var locationTree = new LocationTree();

            using var memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);
            memoryStream.Position = 0;

            using var reader = new StreamReader(memoryStream, Encoding.UTF8);
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(':', 2);
                if (parts.Length != 2) continue;

                var platform = parts[0].Trim();
                var locations = parts[1].Split(',', StringSplitOptions.RemoveEmptyEntries);

                string pattern = @"^(/[a-z]+)+$";
                foreach (var location in locations)
                {
                    var loc = location.Trim().ToLower();
                    if (Regex.IsMatch(loc, pattern)) locationTree.Add(loc, platform);
                }
            }
            _locationTreeService.Replace(locationTree);

            return Ok("Данные загружены");
        }

        [HttpGet]
        public IActionResult GetPlatforms([FromQuery] string? location)
        {
            string pattern = @"^(/[a-z]+)+$";
            if (string.IsNullOrEmpty(location) || !Regex.IsMatch(location.ToLower(), pattern)) return BadRequest("Некорректно задана локация");
            return Ok(_locationTreeService.LocationTree.GetPlatforms(location));
        }
    }
}