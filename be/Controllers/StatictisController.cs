using be.Models;
using be.Services.StatictisService;
using be.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace be.Controllers
{
    [Route("api/statictis")]
    [ApiController]
    public class StatictisController : Controller
    {
        private readonly IStatictisService _statictisService;
        private readonly IConfiguration _configuration;

        public StatictisController(IStatictisService statictisService, IConfiguration configuration)
        {
            _statictisService = statictisService;
            _configuration = configuration;
        }

        [HttpGet("getTestDetails")]
        public async Task<ActionResult> GetTestDetails()
        {
            try
            {
                var result = _statictisService.GetTestDetails();
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
