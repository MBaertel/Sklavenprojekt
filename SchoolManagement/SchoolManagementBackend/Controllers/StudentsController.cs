using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementBackend.InputModels;
using SchoolManagementBackend.OutputModels;
using SchoolManagementDomain.Core.Models.Teachers;
using SchoolManagementInfrastructure.EF;

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
            var student = input.ToEF();
            context.Students.Add(student);
            return Ok(student.Id);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateStudent(Guid id,[FromBody] StudentInput input)
        {
            var student = input.ToEF();
            var efStudent = await context.Students
                .Where(x => x.Id == student.Id)
                .FirstOrDefaultAsync();
            if (efStudent == null) return NotFound($"Student with Id {id} not Found");
            efStudent.Name = student.Name;

            return Ok(StudentDTO.FromEf(efStudent));
        }
    }
}
