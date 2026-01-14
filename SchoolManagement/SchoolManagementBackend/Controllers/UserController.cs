using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagementBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost("validate/{code:string}")]
        public async Task<IActionResult> Validate(string code)
        {
            return Ok(Guid.NewGuid().ToString());
        }
    }
}
