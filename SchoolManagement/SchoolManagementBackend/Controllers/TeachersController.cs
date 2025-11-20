using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagementBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private SchoolManagementContext _dbContext;
        public TeachersController(SchoolManagementContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
