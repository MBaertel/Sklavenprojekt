using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementBackend.InputModels;
using SchoolManagementBackend.OutputModels;
using SchoolManagementInfrastructure.EF;
using SchoolManagementInfrastructure.EF.Models;

namespace SchoolManagementBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController(SchoolManagementContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await context.Students.ToListAsync();
            if (students.Count == 0) return NotFound("No students found.");
            return Ok(students);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetStudent(Guid id)
        {
            var student = await context.Students
                .Where(x => x.Id == id)
                .Include(x => x.ClassStudents)
                .Include(x => x.SubjectStudents)
                .Include(x => x.IndividualExams)
                .FirstOrDefaultAsync();
            if (student == null) return NotFound($"Student with Id {id} not Found");
            return Ok(StudentDTO.FromEf(student));
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] StudentInput input)
        {
            var student = input.ToEf();
            await context.Students.AddAsync(student);
            return Ok(student.Id);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateStudent(Guid id,[FromBody] StudentInput input)
        {
            var student = input.ToEf();
            var efStudent = await context.Students
                .Where(x => x.Id == student.Id)
                .FirstOrDefaultAsync();
            if (efStudent == null) return NotFound($"Student with Id {id} not Found");
            efStudent.Name = student.Name;

            return Ok(StudentDTO.FromEf(efStudent));
        }

        [HttpGet("GetStudents")]
        public async Task<IActionResult> GetStudents([FromQuery]List<Guid> studentIdList)
        {
            var efStudents = new List<EFStudent>();
            var notFound = new List<Guid>();
            foreach (var student in studentIdList)
            {
                var foundEfStudent = await context.Students.SingleOrDefaultAsync(x => x.Id == student);
                if (foundEfStudent == null)
                {
                    notFound.Add(student);
                    continue;
                }
                efStudents.Add(foundEfStudent);
            }
            
            if (notFound.Count > 0)
            {
                var json = JsonSerializer.Serialize(notFound);
                return NotFound($"Following Ids of Subjects not found: {json}");
            }
            return Ok(efStudents);
        }
    }
}
