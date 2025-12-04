using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementBackend.InputModels;
using SchoolManagementInfrastructure.EF;
using SchoolManagementInfrastructure.EF.Models;

namespace SchoolManagementBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController(SchoolManagementContext context) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateClass([FromBody] ClassInput input)
        {
            var efStudents = await context.ClassStudents
                .Where(x => input.Students.Contains(x.StudentId))
                .ToListAsync();
           
            var efTeachers = await context.ClassTeachers
                .Where(x => input.Teachers.Contains(x.TeacherId))
                .ToListAsync();
            
            var efClass = input.ToEF(efStudents, efTeachers);
            context.Add(efClass);
            await context.SaveChangesAsync();
            return Ok(efClass.Id);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateClass(Guid id, [FromBody] ClassInput input)
        {
            var @class = input.ToEF();
            var efClass = await context.Class.FirstOrDefaultAsync(x => x.Id == id);
            if (efClass == null) return NotFound($"Class with Id {id} not Found");
            efClass.Name = @class.Name;
            efClass.ClassStudents = @class.ClassStudents;
            efClass.ClassTeachers = @class.ClassTeachers;
            await context.SaveChangesAsync();
            return Ok(efClass.Id);
        }
        
        [HttpGet("GetClass")]
        public IActionResult GetClass(Guid classId)
        {
            var efClass = context.Class.SingleOrDefault(x => x.Id == classId);
            return Ok(efClass == null ? BadRequest($"Class with Id {classId} not Found") : efClass);
        }

        [HttpGet("GetClasses")]
        public IActionResult GetClasses(List<Guid> classIdList)
        {
            var efClasses = new List<EFClass>();
            var notFound = new List<Guid>();
            foreach (var @class in classIdList)
            {
                var found = context.Class.SingleOrDefault(x => x.Id == @class);
                if (found == null)
                {
                    notFound.Add(@class);
                    continue;
                }
                efClasses.Add(found);
            }
            
            if (notFound.Count > 0)
            {
                var json = JsonSerializer.Serialize(notFound);
                return NotFound($"Following Ids of Subjects not found: {json}");
            }
            return Ok(efClasses.Count == 0 ? BadRequest($"Classes with Id {classIdList} not Found") : efClasses);
        }

        [HttpPut("{classId:guid}/students")]
        public async Task<IActionResult> AddClassToStudents(Guid classId, [FromBody] List<Guid> studentIdList)
        {
            var efClass = await context.Class.Where(x => x.Id == classId)
                .Include(x => x.ClassStudents)
                .FirstOrDefaultAsync();
            if (efClass == null) return NotFound($"Class with Id {classId} not Found");
            
            var students = await context.Students
                .Where(x => studentIdList.Contains(x.Id))
                .Select(x => x.Id)
                .ToListAsync();
            if (students.Count == 0) return NotFound($"No students with specified IDs {studentIdList} found");
            
            var classStudents = students.Select(x => new EFClassStudent(efClass.Id, x)).ToList();
            await context.ClassStudents.AddRangeAsync(classStudents);
            await context.SaveChangesAsync();
            return Ok(efClass);
        }
        
        [HttpDelete("DeleteClasses")]
        public async Task<IActionResult> DeleteClasses(List<Guid> classIdList)
        {
            var efClasses = new List<EFClass>();
            var notFound = new List<Guid>();
            foreach (var @class in classIdList)
            {
                var found = context.Class.SingleOrDefault(x => x.Id == @class);
                if (found == null)
                {
                    notFound.Add(@class);
                    continue;
                }
                efClasses.Add(found);
            }
            
            context.RemoveRange(efClasses);
            await context.SaveChangesAsync().ConfigureAwait(false);
            
            if (notFound.Count > 0)
            {
                var json = JsonSerializer.Serialize(notFound);
                return NotFound($"Classes were deleted, except following Ids of Classes because they were not found: {json}");
            }
            return Ok(efClasses.Count == 0 ? BadRequest($"Classes with Id {classIdList} not Found") : efClasses);
        }
    }
}
