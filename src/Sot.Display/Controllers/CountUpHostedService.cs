using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sot.Display.Services;

namespace Sot.Display.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountUpController : ControllerBase
    {


        private readonly ILogger<CountUpController> _logger;
        private readonly CountUpService _countUpService;

        public CountUpController(ILogger<CountUpController> logger, Services.CountUpService countUpService)
        {
            _logger = logger;
            _countUpService = countUpService;
        }

        [HttpGet("{id}/start")]
        public IActionResult Start(string id)
        {
            _countUpService.Start(id);
            return Ok();
        }

        [HttpGet("{ids}/multiple/start")]
        public IActionResult StartMultiple(string ids)
        {
            foreach (var id in ids.Split(","))
            {
                _countUpService.Start(id);
            }
          
            return Ok();
        }

        [HttpGet("{id}/start/{seconds}")]
        public IActionResult StartWithCountDown(string id, int seconds)
        {
            _countUpService.StartWithCountDown(id, seconds);
            return Ok();
        }

        [HttpGet("{ids}/multiple/start/{seconds}")]
        public IActionResult StartMultiple(string ids, int seconds)
        {
            foreach (var id in ids.Split(","))
            {
                _countUpService.StartWithCountDown(id, seconds);
            }

            return Ok();
        }

        [HttpGet("{id}/stop")]
        public IActionResult Stop(string id)
        {
            _countUpService.Stop(id);
            return Ok();
        }

        [HttpGet("{id}/reset")]
        public IActionResult Reset(string id)
        {
            _countUpService.Reset(id);
            return Ok();
        }
    }
}
