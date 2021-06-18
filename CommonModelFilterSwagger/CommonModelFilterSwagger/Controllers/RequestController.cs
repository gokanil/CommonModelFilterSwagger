using Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommonModelFilterSwagger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        [HttpPost("Create")]
        public IActionResult Create(TestModel testModel)
        {
            return BadRequest();
        }

        [HttpPost("Update")]
        public ActionResult Update(TestModel testModel)
        {
            return BadRequest();
        }
    }
}
