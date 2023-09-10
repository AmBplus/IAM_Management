using api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace api.Areas.Admin.Controllers
{
    [Route("api/admin/{controller}")]
    [ApiController]
    public class AddRollController : ControllerBase
    {
        [Route("home")]
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(200);
        }
    }
}
