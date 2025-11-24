using Microsoft.AspNetCore.Mvc;
using SchoolManagementInfrastructure.EF;

namespace SchoolManagementBackend.Controllers.SetControllers;

[Route("api/set/[controller]")]
[ApiController]
public class SetController(SchoolManagementContext context) : ControllerBase
{
    //Teacher
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