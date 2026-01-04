using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementBackend.InputModels;
using SchoolManagementInfrastructure.EF;

namespace SchoolManagementBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController(SchoolManagementContext context) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateClass([FromBody] ClassInput input)
        {
            //User.Claims.FirstOrDefault(c => c.Type == "internal_id")?.Value;

            var studentIds = await context.Students
                .Where(x => input.Students.Contains(x.Id))
                .Select(x => x.Id)
                .ToListAsync();
            var missingS = input.Students.Except(studentIds).ToList();
            if (missingS.Any()) return BadRequest($"Invalid Student Ids: {string.Join(",", missingS)}");
            
            var teacherIds = await context.Teachers
                .Where(x => input.Teachers.Contains(x.Id))
                .Select(x => x.Id)
                .ToListAsync();
            var missingT = input.Students.Except(teacherIds).ToList();
            if (missingT.Any()) return BadRequest($"Invalid Teacher Ids: {string.Join(",", missingT)}");

            var efClass = input.ToEF();
            context.Add(efClass);
            await context.SaveChangesAsync();
            return Ok(efClass.Id);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateClass(Guid id, [FromBody] ClassInput input)
        {
            return Ok();
        }
    }
}
