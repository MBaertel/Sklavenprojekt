using Microsoft.AspNetCore.Mvc;
using SchoolManagementInfrastructure.EF;
using SchoolManagementInfrastructure.EF.Models;

namespace SchoolManagementBackend.Controllers.GetControllers
{
    [Route("api/get/[controller]")]
    [ApiController]
    public class GetController(SchoolManagementContext context) : ControllerBase
    {
        [HttpGet("GetTeacher")]
        public IActionResult GetTeacher(Guid teacherId)
        {
            var teacherEf = context.Teachers.Single(x => x.Id == teacherId);
            return Ok(teacherEf);
        }

        [HttpGet("GetTeachers")]
        public IActionResult GetTeachers(List<Guid> teacherIdList)
        {
            var teachers = new List<EFTeacher>();
            foreach (var teacher in teacherIdList)
            {
                var teacherEf = context.Teachers.Where(x => x.Id == teacher).ToList();
                teachers.AddRange(teacherEf);
            }
            return Ok(teachers);
        }
        
        [HttpGet("GetSubject")]
        public IActionResult GetSubject(Guid subjectId)
        {
            var subjectEf = context.Subjects.Single(x => x.Id == subjectId);
            return Ok(subjectEf);
        }

        [HttpGet("GetSubjects")]
        public IActionResult GetSubjects(List<Guid> subjectIdList)
        {
            var efSubjects = new List<EFSubject>();
            foreach (var subject in subjectIdList)
            {
                var foundEfSubjects = context.Subjects.Where(x => x.Id == subject).ToList();
                efSubjects.AddRange(foundEfSubjects);
            }
            return Ok(efSubjects);
        }
        
        [HttpGet("GetStudent")]
        public IActionResult GetStudent(Guid studentId)
        {
            var efStudent = context.Students.Single(x => x.Id == studentId);
            return Ok(efStudent);
        }

        [HttpGet("GetStudents")]
        public IActionResult GetStudents(List<Guid> studentIdList)
        {
            var efStudents = new List<EFStudent>();
            foreach (var student in studentIdList)
            {
                var foundEfStudents = context.Students.Where(x => x.Id == student).ToList();
                efStudents.AddRange(foundEfStudents);
            }
            return Ok(efStudents);
        }
        
        [HttpGet("GetClass")]
        public IActionResult GetClass(Guid classId)
        {
            var efClass = context.Class.Single(x => x.Id == classId);
            return Ok(efClass);
        }

        [HttpGet("GetClasses")]
        public IActionResult GetClasses(List<Guid> classIdList)
        {
            var efClasses = new List<EFClass>();
            foreach (var @class in classIdList)
            {
                var found = context.Class.Where(x => x.Id == @class).ToList();
                efClasses.AddRange(found);
            }
            return Ok(efClasses);
        }

        [HttpGet("GetExam")]
        public IActionResult GetExam(Guid examId)
        {
            var efClassExam = context.ClassExams.Single(x => x.Id == examId);
            return Ok(efClassExam);
        }

        [HttpGet("GetExams")]
        public IActionResult GetExams(List<Guid> examIdList)
        {
            var efExams = new List<EFClassExam>();
            foreach (var exam in examIdList)
            {
                var found = context.ClassExams.Where(x => x.Id == exam).ToList();
                efExams.AddRange(found);
            }
            return Ok(efExams);
        }

        [HttpGet("GetIndividualExam")]
        public IActionResult GetIndividualExam(Guid examId)
        {
            var efExam = context.IndividualExams.Single(x => x.Id == examId);
            return Ok(efExam);
        }

        [HttpGet("GetIndividualExams")]
        public IActionResult GetIndividualExams(List<Guid> examIds)
        {
            var efIndividualExams = new List<EFIndividualExam>();
            foreach (var exam in examIds)
            {
                var found = context.IndividualExams.Where(x => x.Id == exam).ToList();
                efIndividualExams.AddRange(found);
            }
            return Ok(efIndividualExams);
        }
        
        [HttpGet("GetExamImage")]
        public IActionResult GetExamImage(Guid imageId)
        {
            var efImage = context.ExamImages.Single(x => x.Id == imageId);
            return Ok(efImage);
        }

        [HttpGet("GetExamImages")]
        public IActionResult GetExamImages(List<Guid> imageIds)
        {
            var efExamImages = new List<EFExamImage>();
            foreach (var image in imageIds)
            {
                var found = context.ExamImages.Where(x => x.Id == image).ToList();
                efExamImages.AddRange(found);
            }
            return Ok(efExamImages);
        }
    }
}
