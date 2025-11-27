using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementBackend.InputModels;
using SchoolManagementBackend.OutputModels;
using SchoolManagementDomain.Core.Models.Teachers;
using SchoolManagementInfrastructure.EF;
using SchoolManagementInfrastructure.EF.Models;
using System.Collections.Generic;

namespace SchoolManagementBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController(SchoolManagementContext context) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetTeachers()
        {
            var teachers = context.Teachers.ToList();
            return Ok(teachers);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetTeacher(Guid id)
        {
            var teacherEf = context.Teachers
                .Include(x => x.ClassTeachers)
                .Include(x => x.Subjects)
                .Single(x => x.Id == id);
            if (teacherEf == null) return NotFound();
            return Ok(TeacherDTO.FromEf(teacherEf));
        }

        [HttpPost]
        public async Task<IActionResult> PostTeacher([FromBody]TeacherInput teacherInput)
        {
            var teacher = teacherInput.ToEF();
            context.Add(teacher);
            await context.SaveChangesAsync();
            return Ok(teacher.Id);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTeacher(Guid teacherId, [FromBody] TeacherInput teacherInput)
        {
            var teacher = teacherInput.ToEF();
            var efTeacher = context.Teachers.Where(x => x.Id == teacherId).FirstOrDefault();
            if (efTeacher != null) return NotFound($"Teacher with Id {teacherId} not Found");
            efTeacher.Name = teacherInput.Name;
            await context.SaveChangesAsync();
            return Ok(teacher.Id);
        }

        [HttpPut("{teacherId:guid}/classes")]
        public async Task<IActionResult> AddClassToTeacher(Guid teacherId, [FromBody] ICollection<Guid> classIds)
        {
            var efTeacher = context.Teachers.Where(x => x.Id == teacherId)
                .Include(x => x.ClassTeachers)
                .FirstOrDefault();
            if (efTeacher != null) return NotFound($"Teacher with Id {teacherId} not Found");

            var classes = await context.Class.Where(x => classIds.Contains(x.Id)).ToListAsync();
            if (classes.Count == 0) return NotFound("No classes with specified IDs found");

            var classTeachers = classes.Select(x => new EFClassTeacher()
            {
                Class = x,
                Teacher = efTeacher
            });
            foreach (var classTeacher in classTeachers)
            {
                efTeacher.ClassTeachers.Add(classTeacher);
            }
            await context.SaveChangesAsync();
            return Ok(efTeacher);
        }
    }
}
