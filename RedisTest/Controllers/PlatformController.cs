using Microsoft.AspNetCore.Mvc;
using RedisTest.Data;
using RedisTest.Models;

namespace RedisTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformRepo _platformRepo;

        public PlatformController(IPlatformRepo platformRepo)
        {
            _platformRepo = platformRepo;
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<Platform> GetPlatformById(string id)
        {
            Platform platform = _platformRepo.GetPlatform(id);

            if (platform == null)
            {
                return NotFound();
            }

            return Ok(platform);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Platform>> GetAllPlatforms()
        {
            return Ok(_platformRepo.GetAllPlatforms());
        }

        [HttpPost]
        public ActionResult<Platform> CreatePlatform(Platform platform)
        {
            _platformRepo.CreatePlatform(platform);
            return CreatedAtRoute(nameof(GetPlatformById), new { id = platform.Id }, platform);
        }
    }
}
