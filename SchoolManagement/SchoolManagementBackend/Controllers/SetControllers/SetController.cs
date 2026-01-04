using Microsoft.AspNetCore.Mvc;
using SchoolManagementBackend.InputModels;
using SchoolManagementInfrastructure.EF;
using SchoolManagementInfrastructure.EF.Models;

namespace SchoolManagementBackend.Controllers.SetControllers;

[Route("api/set/[controller]")]
[ApiController]
public class SetController(SchoolManagementContext context) : ControllerBase
{
    [HttpPost("SetTeacher")]
    public async Task<IActionResult> CreateTeacher([FromBody]TeacherInput teacherInput)
    {
        var teacher = teacherInput.ToEF();
        context.Add(teacher);
        await context.SaveChangesAsync();
        return Ok(teacher.Id);
    }

    /*[HttpPut("SetTeacher/{teacherId:guid}")]
    public async Task<IActionResult> UpdateTeacher(Guid teacherId,[FromBody]TeacherInput teacherInput)
    {
        var teacher = teacherInput.ToEF();
        var efTeacher = context.Teachers.Where(x => x.Id == teacherId).FirstOrDefault();
        if (efTeacher != null) NotFound($"Teacher with Id {teacherId} not Found");
        efTeacher.Name = teacherInput.Name;
        await context.SaveChangesAsync();
        Ok(teacher.Id);
    }*/

    //public async Task<IActionResult> AddClassToTeacher()
    //ClassTeacher
    //BaseSubject
    //Subject
    //SubjectStudent
    //Student
    //ClassStudent
    //ClassExam
    //ExamImages
    //IndividualExam
    //Class
}